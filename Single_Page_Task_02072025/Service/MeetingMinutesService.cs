using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Single_Page_Task_02072025.Data;
using Single_Page_Task_02072025.Models;
using Single_Page_Task_02072025.Service.Interface;
using System.Data;

namespace Single_Page_Task_02072025.Service
{
    public class MeetingMinutesService : IMeetingMinutesService
    {
        private readonly AppDbContext _db;
        public MeetingMinutesService(AppDbContext context)
        {
            _db = context;
        }
        public async Task<int> SaveAsync(MeetingMinute master, IEnumerable<MeetingMinuteDetail> details)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }
            if (details == null || details.Count() == 0)
            {
                details = Enumerable.Empty<MeetingMinuteDetail>();
            }

            await using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@CustomerType", master.CustomerType),
                    new SqlParameter("@CustomerId", master.CustomerId),
                    new SqlParameter("@MeetingDate", (object?)master.MeetingDate ?? DBNull.Value),
                    new SqlParameter("@MeetingTime", (object?)master.MeetingTime ?? DBNull.Value),
                    new SqlParameter("@MeetingPlace", master.MeetingPlace),
                    new SqlParameter("@AttendsFromClientSide", master.AttendsFromClientSide),
                    new SqlParameter("@AttendsFromHostSide", master.AttendsFromHostSide),
                    new SqlParameter("@MeetingAgenda", master.MeetingAgenda),
                    new SqlParameter("@MeetingDiscussion", master.MeetingDiscussion),
                    new SqlParameter("@MeetingDecision", master.MeetingDecision),
                    new SqlParameter
                    {
                        ParameterName = "@NewId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    }
                };

                await _db.Database.ExecuteSqlRawAsync(@"EXEC dbo.Meeting_Minutes_Master_Save_SP 
                            @CustomerType, @CustomerId, @MeetingDate, @MeetingTime, @MeetingPlace,
                            @AttendsFromClientSide, @AttendsFromHostSide, @MeetingAgenda,
                            @MeetingDiscussion, @MeetingDecision, @NewId OUTPUT",
                            parameters
                );

                var mId = parameters.FirstOrDefault(p => p.ParameterName == "@NewId");
                if (mId != null)
                {
                    master.MeetingMinuteId = (int)(mId.Value ?? 0);
                }
                if (master.MeetingMinuteId == 0)
                {
                    throw new Exception("Failed to insert master record.");
                }


                DataTable detailTable = new DataTable();
                detailTable.Columns.Add("MeetingMinuteId", typeof(int));
                detailTable.Columns.Add("ProductServiceId", typeof(int));
                detailTable.Columns.Add("Quantity", typeof(decimal));
                detailTable.Columns.Add("Unit", typeof(string));

                foreach (var d in details.Where(x => x.ProductServiceId > 0))
                {
                    detailTable.Rows.Add(master.MeetingMinuteId, d.ProductServiceId, d.Quantity, d.Unit);
                }

                SqlParameter tableValuedParameters = new SqlParameter("@Details", SqlDbType.Structured)
                {
                    TypeName = "MeetingMinuteDetailType",
                    Value = detailTable
                };

                await _db.Database.ExecuteSqlRawAsync("EXEC dbo.Meeting_Minutes_Details_Save_SP @Details", tableValuedParameters);

                await transaction.CommitAsync();
                return master.MeetingMinuteId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}

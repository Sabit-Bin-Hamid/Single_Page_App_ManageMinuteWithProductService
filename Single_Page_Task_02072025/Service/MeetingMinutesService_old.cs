//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using Single_Page_Task_02072025.Data;
//using Single_Page_Task_02072025.Models;
//using Single_Page_Task_02072025.Service.Interface;
//using System.Data;

//namespace Single_Page_Task_02072025.Service
//{
//    public class MeetingMinutesService_old : IMeetingMinutesService
//    {
//        private readonly AppDbContext _db;
//        public MeetingMinutesService_old(AppDbContext context)
//        {
//            _db = context;
//        }
//        public async Task<int> SaveAsync(MeetingMinute master, IEnumerable<MeetingMinuteDetail> details)
//        {
//            if (master == null)
//            {
//                throw new ArgumentNullException(nameof(master));
//            }
//            if(details == null || details.Count()==0)
//            {
//                details = Enumerable.Empty<MeetingMinuteDetail>();
//            }
//            await using var transaction = await _db.Database.BeginTransactionAsync();

//            try
//            {
//                var parameters = new[]
//                {
//                    new SqlParameter("@CustomerType", master.CustomerType),
//                    new SqlParameter("@CustomerId",master.CustomerId),
//                    new SqlParameter("@MeetingDate",(object?)master.MeetingDate ?? DBNull.Value),
//                    new SqlParameter("@MeetingTime",(object?)master.MeetingTime ?? DBNull.Value),
//                    new SqlParameter("@MeetingPlace",master.MeetingPlace),
//                    new SqlParameter("@AttendsFromClientSide",master.AttendsFromClientSide ),
//                    new SqlParameter("@AttendsFromHostSide", master.AttendsFromHostSide ),
//                    new SqlParameter("@MeetingAgenda", master.MeetingAgenda ),
//                    new SqlParameter("@MeetingDiscussion", master.MeetingDiscussion ),
//                    new SqlParameter("@MeetingDecision", master.MeetingDecision ),
//                    new SqlParameter{ ParameterName = "@NewId",SqlDbType = SqlDbType.Int,
//                                      Direction = ParameterDirection.Output
//                    }
//                };

//                await _db.Database.ExecuteSqlRawAsync( @"EXEC dbo.Meeting_Minutes_Master_Save_SP 
//                                @CustomerType, @CustomerId, @MeetingDate, @MeetingTime, @MeetingPlace,
//                                @AttendsFromClientSide, @AttendsFromHostSide, @MeetingAgenda,
//                                @MeetingDiscussion, @MeetingDecision, @NewId OUTPUT",
//                                parameters
//                );

//                var mId = parameters.FirstOrDefault(p => p.ParameterName == "@NewId");
//                if (mId != null) master.MeetingMinuteId = (int)(mId.Value ?? 0);
//                if (master.MeetingMinuteId == 0) throw new Exception("Failed to insert record.");
                
//                foreach (var d in details.Where(x => x.ProductServiceId > 0))
//                {
//                    var detailParams = new[]
//                    {
//                        new SqlParameter("@MeetingMinuteId", master.MeetingMinuteId),
//                        new SqlParameter("@ProductServiceId", d.ProductServiceId),
//                        new SqlParameter("@Quantity", d.Quantity),
//                        new SqlParameter("@Unit", d.Unit)
//                    };

//                    await _db.Database.ExecuteSqlRawAsync(@"EXEC dbo.Meeting_Minutes_Details_Save_SP 
//                        @MeetingMinuteId, @ProductServiceId, @Quantity,@Unit",
//                        detailParams);
//                }
//                await transaction.CommitAsync();
//                return master.MeetingMinuteId;
//            }
//            catch
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using Single_Page_Task_02072025.Data;
using System.Diagnostics;

namespace Single_Page_Task_02072025.Service.DataAccessServie
{
    public class DataAccessService : IDataAccessService
    {
        private readonly AppDbContext dbModel;

        public DataAccessService(AppDbContext dbModel)
        {
            this.dbModel = dbModel;
        }

        async Task<IList<T>> IDataAccessService.GetListDataAsync<T>(FormattableString storedProc)
        {
            try
            {
                return await dbModel.Set<T>().FromSqlInterpolated(storedProc).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}

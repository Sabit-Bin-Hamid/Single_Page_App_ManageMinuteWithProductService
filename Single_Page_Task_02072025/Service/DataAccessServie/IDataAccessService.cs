namespace Single_Page_Task_02072025.Service.DataAccessServie
{
    public interface IDataAccessService
    {
        Task<IList<T> >GetListDataAsync<T>(FormattableString storedProc) where T : class;
    }
}

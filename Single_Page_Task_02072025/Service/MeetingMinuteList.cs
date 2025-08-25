using Microsoft.EntityFrameworkCore;
using Single_Page_Task_02072025.Data;
using Single_Page_Task_02072025.Models;
using Single_Page_Task_02072025.Models.DTO;
using Single_Page_Task_02072025.Service.DataAccessServie;
using Single_Page_Task_02072025.Service.Interface;

namespace Single_Page_Task_02072025.Service
{
    public class MeetingMinuteList : IMeetingMinuteList

    {

        private readonly IDataAccessService _service;
        private readonly AppDbContext _db;

        public MeetingMinuteList(IDataAccessService service, AppDbContext _db)
        {
            _service = service;
            this._db = _db;
        }
        public async Task<IList<MeetingMinute>> GetMeetingMinuteListAsync()
        {
            var obj= await _service.GetListDataAsync<MeetingMinute>($"EXEC Get_All_Meeting_Minutes_SP");
            return obj;
        }

        public IList<ProductDTO> GetProductListByMeeting(int id)
        {
            var result = (from d in _db.Meeting_Minutes_Details_Tbl
                          join p in _db.Products_Service_Tbl
                              on d.ProductServiceId equals p.ProductServiceId
                          where d.MeetingMinuteId == id
                          select new ProductDTO
                          {
                              MeetingMinuteDetailId = d.MeetingMinuteDetailId,
                              MeetingMinuteId = d.MeetingMinuteId,
                              ProductServiceId = d.ProductServiceId,
                              Quantity = d.Quantity,
                              Unit = d.Unit,
                              Name = p.Name
                          }).ToList();

            return result;
        }
    }
}

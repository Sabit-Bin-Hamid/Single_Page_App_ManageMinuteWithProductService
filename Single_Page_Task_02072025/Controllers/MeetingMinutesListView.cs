using Microsoft.AspNetCore.Mvc;
using Single_Page_Task_02072025.Data;
using Single_Page_Task_02072025.Models.DTO;
using Single_Page_Task_02072025.Service.Interface;

namespace Single_Page_Task_02072025.Controllers
{
    public class MeetingMinutesListView : Controller
    {
        private readonly AppDbContext _db;
        private readonly IMeetingMinuteList _mml;
        public MeetingMinutesListView(AppDbContext context ,IMeetingMinuteList meetingMinuteList)
        {
            _db = context;
            _mml= meetingMinuteList;
        }
        public async Task<IActionResult >IndexList()
        {
            var mmlist =await _mml.GetMeetingMinuteListAsync();

            return View(mmlist);
        }

        public  IActionResult MeetingProducts(int MeetingMinuteId)
        {
            var productobj = _mml.GetProductListByMeeting(MeetingMinuteId);
            return View(productobj);
        }
    }
}

using Single_Page_Task_02072025.Models;
using Single_Page_Task_02072025.Models.DTO;

namespace Single_Page_Task_02072025.Service.Interface
{
    public interface IMeetingMinuteList
    {
        Task<IList<MeetingMinute>> GetMeetingMinuteListAsync();
        IList<ProductDTO> GetProductListByMeeting(int id);
    }
}

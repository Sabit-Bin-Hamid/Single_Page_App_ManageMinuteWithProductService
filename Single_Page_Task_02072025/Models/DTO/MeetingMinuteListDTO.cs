namespace Single_Page_Task_02072025.Models.DTO
{

    public class MeetingMinuteListDTO
    {
        public int MeetingMinuteId { get; set; }
        public string CustomerType { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? MeetingDate { get; set; }
        public TimeSpan? MeetingTime { get; set; }
        public string MeetingPlace { get; set; }
        public string AttendsFromClientSide { get; set; }
        public string AttendsFromHostSide { get; set; }
        public string MeetingAgenda { get; set; }
        public string MeetingDiscussion { get; set; }
        public string MeetingDecision { get; set; }
    }


}

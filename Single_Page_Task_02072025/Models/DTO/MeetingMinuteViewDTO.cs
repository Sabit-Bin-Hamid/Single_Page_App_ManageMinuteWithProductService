namespace Single_Page_Task_02072025.Models.DTO
{
    public class MeetingMinuteViewDTO
    {
        public MeetingMinute? Meeting { get; set; }
        public List<MeetingMinuteDetail>? Details { get; set; } 

        public int? SelectedProductId { get; set; }
        public decimal? Quantity { get; set; }
        public string? Unit { get; set; }

        public List<string> setDataSetDataAndCheckForErrors()
        {
            List<string> errList = new();
            if(Meeting == null)
            {
                errList.Add("Invalid form.");
            }

            if (Meeting?.CustomerId < 1)
            {
                errList.Add("Customer Name is required");
            }
            return errList;
        }
    }
}

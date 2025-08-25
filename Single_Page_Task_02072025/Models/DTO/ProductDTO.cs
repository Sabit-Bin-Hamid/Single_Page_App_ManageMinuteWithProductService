using System.ComponentModel.DataAnnotations;

namespace Single_Page_Task_02072025.Models.DTO
{
    public class ProductDTO
    {
        public int MeetingMinuteDetailId { get; set; }
        public int MeetingMinuteId { get; set; }

        public int ProductServiceId { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public decimal Quantity { get; set; }   
    }
}

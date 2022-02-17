using DOMAIN.Interface;

namespace APPLICATION.Hotel.Models
{
    public class HotelResponseDto : HotelDto, ITrackCreated, ITrackUpdated
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}

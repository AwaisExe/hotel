using DOMAIN.Interface;

namespace APPLICATION.Booking.Models
{
    public class BookingResponseDto : BookingDto, ITrackCreated, ITrackUpdated
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}

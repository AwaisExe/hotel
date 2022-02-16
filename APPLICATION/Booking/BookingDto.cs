using DOMAIN.Entities;

namespace APPLICATION.Booking
{
    public class BookingDto : BaseEntity<int>
    {
        public int? HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PassportNo { get; set; }
        public int NoOfPersons { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}

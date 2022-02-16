using DOMAIN.Entities;

namespace APPLICATION.Hotel
{
    public class HotelDto : BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal AverageRating { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Lattitude { get; set; }
    }
}

using DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Hotel : BaseEntity<int>, ITrackCreated, ITrackUpdated
    {
        public string Name { get; set; }
        public decimal AverageRating { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Lattitude { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}

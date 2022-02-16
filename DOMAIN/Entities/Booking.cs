using DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Booking : BaseEntity<int>, ITrackCreated, ITrackUpdated
    {
        public int? HotelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PassportNo { get; set; }
        public int NoOfPersons { get; set; }
        public DateTime CheckInDate{ get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Hotel Hotel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Interface
{
    public interface ITrackCreated
    {
         DateTime CreatedOn { get; set; }
         string CreatedBy { get; set; }
    }
}

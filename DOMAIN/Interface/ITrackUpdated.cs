using System;

namespace DOMAIN.Interface
{
    public interface ITrackUpdated
    {
        DateTime? UpdatedOn { get; set; }
        string UpdatedBy { get; set; }
    }
}

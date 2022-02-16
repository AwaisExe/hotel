using DOMAIN.Interface;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOMAIN.Entities
{
    public abstract class BaseEntity<TKey> : ISoftDelete
    {
        [Column(Order = 1)]
        public TKey Id { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }     

    }
}

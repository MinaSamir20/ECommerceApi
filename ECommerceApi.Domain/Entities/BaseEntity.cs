#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Domain.Entites
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
    }
}

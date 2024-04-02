using ECommerceApi.Domain.Entites;

namespace ECommerceApi.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? CategoryName { get; set; }

        /*-------- Relations --------*/
        public ICollection<Product>? Products { get; set; }
    }
}

using ECommerceApi.Domain.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int MinimumQuantity { get; set; }
        public double DiscountRate { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }

        /*-------- Relations --------*/
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

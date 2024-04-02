using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Application.DTOs.Dtos
{
    public class ProductDto
    {
        [Required(ErrorMessage = "ProductName is Required")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "MinimumQuantity is Required")]
        public int MinimumQuantity { get; set; }
        public double DiscountRate { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Image is Required")]
        public IFormFile? Image { get; set; }
    }
}

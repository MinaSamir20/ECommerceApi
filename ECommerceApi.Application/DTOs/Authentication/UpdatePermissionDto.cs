using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Application.DTOs.Authentication
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "Username is Required")]
        public string? Username { get; set; }
    }
}

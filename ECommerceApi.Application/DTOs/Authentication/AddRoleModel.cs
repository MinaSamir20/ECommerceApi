using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Application.DTOs.Authentication
{
    public class AddRoleModel
    {
        [Required(ErrorMessage = "User Id is Required")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; }
    }
}

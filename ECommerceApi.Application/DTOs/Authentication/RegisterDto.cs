using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Application.DTOs.Authentication
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is Required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(8, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; }
    }
}

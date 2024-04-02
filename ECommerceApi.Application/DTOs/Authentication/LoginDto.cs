﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Application.DTOs.Authentication
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleRESTApi.DTO
{
    public class AspUserDTO
    {
        [Key]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string? PasswordHash { get; set; }

        // fitur reset
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
    }
}

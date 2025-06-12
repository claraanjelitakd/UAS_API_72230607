using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SimpleRESTApi.Models
{
    public class AspUser
    {
    [Key]
    public string Username { get; set; } = null!;
    public string password { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;
    //fitur reset
    public string? ResetToken { get; set; }

    public DateTime? ResetTokenExpiry { get; set; }
    }
    
}
// filepath: f:\SEMESTER 4\WEB RESTFUL API\UserManagement\WebApplication1\DTO\AspUserLoginDTO.cs
using System.ComponentModel.DataAnnotations;

namespace SimpleRESTApi.DTO
{
    public class AspUserLoginDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
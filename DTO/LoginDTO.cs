using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.DTO
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
    }
}
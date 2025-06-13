using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace UAS_POS_CLARA.Models
{
    public class AspUser
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
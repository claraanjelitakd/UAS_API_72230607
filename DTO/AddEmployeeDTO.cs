using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.DTO
{
    public class AddEmployeeDTO
    {
        public string? Name { get; set; }= null!;
        public string? PhoneNumber { get; set; }= null!;
        public string Email { get; set; } = null!;
        public string Position { get; set; }= null!;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.DTO
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string? Name { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Address { get; set; } = null!;
    }
}
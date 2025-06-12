using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.DTO
{
    public class CustomersDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? ConctactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
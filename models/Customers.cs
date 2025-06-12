using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Models
{
    public class Customers
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? ConctactNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public IEnumerable<Sales> Sales { get; set; } = new List<Sales>();
    }
}
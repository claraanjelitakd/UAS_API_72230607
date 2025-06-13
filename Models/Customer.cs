using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace UAS_POS_CLARA.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public IEnumerable<Sale> Sales { get; set; } = new List<Sale>();
    }
}
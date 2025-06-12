using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Models
{
    public class Sales
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public Customers Customer { get; set; } = null!;
        public DateTime? SaleDate { get; set; }
        public decimal TotalAmount { get; set; }

        public IEnumerable<SaleItems> SaleItems { get; set; } = new List<SaleItems>();
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Models
{
    public class SaleItems
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public Sales Sale { get; set; } = null!;
        public int ProductId { get; set; }
        public Products Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
    }
}
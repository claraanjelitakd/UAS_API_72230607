using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace UAS_POS_CLARA.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<SaleItems>? SaleItems { get; set; } = new List<SaleItems>();
    }
}
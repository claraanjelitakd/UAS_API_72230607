using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
        public string? Description { get; set; }
        public CategoryDTO? Category { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.DTO
{
    public class SaleItemsDTO
    {
        public int SaleItemID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAS_POS_CLARA.Models
{
    public class SaleItems
    {
        [Key]
        public int SaleItemID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("SaleID")]
        public Sale? Sale { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
    }
}
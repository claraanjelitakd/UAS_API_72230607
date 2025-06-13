using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace UAS_POS_CLARA.Models
{
    public class Sale
    {
        [Key]
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }

        // Navigation property
        public IEnumerable<SaleItems> SaleItems { get; set; } = new List<SaleItems>();
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
    }
}
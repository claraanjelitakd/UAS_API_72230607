using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.DTO
{
    public class SaleDTO
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public CustomerDTO? Customer { get; set; }
        public List<SaleItemsDTO> SaleItems { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.DTO
{
    public class GetSaleItemsDTO
    {
        public int SaleItemID { get; set; }
        public AddSaleDTO? Sale { get; set; }
        public AddProductDTO? Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
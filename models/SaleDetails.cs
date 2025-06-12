using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.Models
{
    public class SaleDetails
    {
     public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set; }
        public List<SaleItemDetail> Items { get; set; } = new List<SaleItemDetail>();
    }
    public class SaleItemDetail
    {
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
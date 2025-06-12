using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Models
{
    public class ViewSalesProductWithCustomers
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } 
        public string ConctactNumber { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public int SaleItemId { get; set; }
        public int Quantity { get; set; }
    }
}
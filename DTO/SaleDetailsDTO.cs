using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.DTO
{
    public class SaleDetailsDTO
    {
        public int SaleId { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<SaleItemDetailDTO> Items { get; set; } = new();
    }
    public class SaleItemDetailDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
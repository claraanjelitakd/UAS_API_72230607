using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.DTO
{
    public class SalesAddDTO
    {
        public int CustomerId { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
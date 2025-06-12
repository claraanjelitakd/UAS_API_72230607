using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class SalesEF:ISales
    {
        private readonly ApplicationDbContext _context;

        public SalesEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public void deleteSales(int SalesID)
        {
            var sale = _context.Sales.Find(SalesID);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }
        }
        IEnumerable<Sales> ISales.GetSales()
        {
              return _context.Sales
            .Include (s => s.Customer)
            .Include (s => s.SaleItems)
            .ThenInclude(si => si.Product)
            .ToList();
        }

        Sales ISales.GetSalesById(int SalesID)
        {
            return _context.Sales.FirstOrDefault(s => s.SaleId == SalesID);
        }

        public Sales addSales(Sales Sales)
        {
            _context.Sales.Add(Sales);
            _context.SaveChanges();
            return Sales;
        }

        public Sales updateSales(Sales Sales)
        {
            var existing = _context.Sales.Find(Sales.SaleId);
            if (existing == null) return null;

            existing.CustomerId = Sales.CustomerId;
            existing.SaleDate = Sales.SaleDate;
            existing.TotalAmount = Sales.TotalAmount;

            _context.SaveChanges();
            return existing;
        }

        IEnumerable<SaleDetails> ISales.GetSalesWithDetails()
        {
            var result = from s in _context.Sales
                         join c in _context.Customers on s.CustomerId equals c.CustomerId
                         join si in _context.SaleItems on s.SaleId equals si.SaleId
                         join p in _context.Products on si.ProductId equals p.ProductId
                         select new SaleDetails
                         {
                             SaleId = s.SaleId,
                             SaleDate = s.SaleDate ?? DateTime.MinValue,
                             CustomerName = c.CustomerName,
                             ProductName = p.ProductName,
                             Quantity = si.Quantity,
                             Price = si.Price,
                             TotalAmount = s.TotalAmount
                         };

            return result.ToList();
        }

        SaleDetails ISales.GetSaleWithDetailsById(int SaleId)
        {
            var result = (from s in _context.Sales
                          join c in _context.Customers on s.CustomerId equals c.CustomerId
                          join si in _context.SaleItems on s.SaleId equals si.SaleId
                          join p in _context.Products on si.ProductId equals p.ProductId
                          where s.SaleId == SaleId
                          select new SaleDetails
                          {
                              SaleId = s.SaleId,
                              SaleDate = s.SaleDate ?? DateTime.MinValue,
                              CustomerName = c.CustomerName,
                              ProductName = p.ProductName,
                              Quantity = si.Quantity,
                              Price = si.Price,
                              TotalAmount = s.TotalAmount
                          }).FirstOrDefault();

            return result;
        }
    }
}
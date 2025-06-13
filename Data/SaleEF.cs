using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public class SaleEF : ISale
    
    {
        private readonly ApplicationDbContext _context;

        public SaleEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Sale> GetAllSales()
        {
            return _context.Sales
                .Include(s => s.SaleItems) // Include related SaleItems
                .ToList();
        }

        public Sale GetSaleById(int saleId)
        {
            var sale = _context.Sales
                .Include(s => s.Customer) 
                .FirstOrDefault(s => s.SaleID == saleId);

            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found.");
            }
            return sale;
        }

        public Sale AddSale(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                _context.SaveChanges();
                return sale;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                throw new Exception("Database update error while adding sale", dbEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding the sale", ex);
            }
        }

        public Sale UpdateSale(Sale sale)
        {
            var existingSale = GetSaleById(sale.SaleID);
            if (existingSale == null)
            {
                throw new KeyNotFoundException("Sale not found.");
            }

            try
            {
                _context.Entry(existingSale).CurrentValues.SetValues(sale);
                _context.SaveChanges();
                return existingSale;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                throw new Exception("Database update error while updating sale", dbEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating the sale", ex);
            }
        }

        public void DeleteSale(int saleId)
        {
            var sale = GetSaleById(saleId);
            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found.");
            }

            try
            {
                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                throw new Exception("Database update error while deleting sale", dbEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while deleting the sale", ex);
            }
        }

        public Sale GetInvoiceById(int SaleID)
        {
            return _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleItems)
                    .ThenInclude(si => si.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefault(s => s.SaleID == SaleID);
        }

        public IEnumerable<Sale> GetAllOfSales()
        {
            var sales = _context.Sales
                .Include(s => s.SaleItems)
                .ThenInclude(si => si.Product)
                .ThenInclude(p => p.Category)
                .Include(s => s.Customer)
                .ToList();
            return sales;
        }
    }
}
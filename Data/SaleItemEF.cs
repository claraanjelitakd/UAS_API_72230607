using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public class SaleItemEF : ISaleItem
    {
        private readonly ApplicationDbContext _context;
        public SaleItemEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public SaleItems AddSaleItem(SaleItems saleItem)
        {
            try
            {
                _context.SaleItems.Add(saleItem);
                _context.SaveChanges();
                return saleItem;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                throw new Exception("Database update error while adding sale item", dbEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding the sale item", ex);
            }

        }

        public void DeleteSaleItem(int saleItemId)
        {
            var saleItem = GetSaleItemById(saleItemId);
            if (saleItem == null)
            {
                throw new KeyNotFoundException("Sale item not found.");
            }
            try
            {
                _context.SaleItems.Remove(saleItem);
                _context.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                throw new Exception("Database update error while deleting sale item", dbEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while deleting the sale item", ex);
            }
        }

        public IEnumerable<SaleItems> GetAllSaleItems()
        {
            var saleItems = from c in _context.SaleItems
            .Include(c => c.Product)
            .Include(c => c.Sale)
                            orderby c.SaleItemID descending
                            select c;
            return saleItems;
        }

        public SaleItems GetSaleItemById(int saleItemId)
        {
            var saleItem = _context.SaleItems
                .Include(c => c.Product)
                .Include(c => c.Sale)
                .FirstOrDefault(c => c.SaleItemID == saleItemId);
            if (saleItem == null)
            {
                throw new KeyNotFoundException("Sale item not found.");
            }
            return saleItem;
        }

        public SaleItems UpdateSaleItem(SaleItems saleItem)
        {
            var existingSaleItem = GetSaleItemById(saleItem.SaleItemID);
            if (existingSaleItem == null)
            {
                throw new KeyNotFoundException("Sale item not found.");
            }
            try
            {
                existingSaleItem.Quantity = saleItem.Quantity;
                existingSaleItem.Price = saleItem.Price;
                existingSaleItem.ProductID = saleItem.ProductID;
                existingSaleItem.SaleID = saleItem.SaleID;
                _context.SaveChanges();
                return existingSaleItem;
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update exceptions
                throw new Exception("Database update error while updating sale item", dbEx);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating the sale item", ex);
            }
        }
    }
}
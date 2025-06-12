using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class SaleItemsEF:ISaleItems
    {
        private readonly ApplicationDbContext _context;

        public SaleItemsEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public void deleteSaleItems(int SaleItemID)
        {
            var saleItem = _context.SaleItems.FirstOrDefault(si => si.SaleItemId == SaleItemID);
            if (saleItem != null)
            {
                _context.SaleItems.Remove(saleItem);
                _context.SaveChanges();
            }
        }

        IEnumerable<SaleItems> ISaleItems.GetSaleItems()
        {
        return _context.SaleItems
        .Include(si => si.Product)  // ini penting untuk ambil nama produk
        .Include(si => si.Sale)     // kalau kamu mau akses data sales juga
        .ToList();

        }

        SaleItems ISaleItems.GetSaleItemsById(int SaleItemID)
        {
            return _context.SaleItems.FirstOrDefault(si => si.SaleItemId == SaleItemID);
        }

        public SaleItems addSaleItems(SaleItems SaleItems)
        {
            _context.SaleItems.Add(SaleItems);
            _context.SaveChanges();
            return SaleItems;
        }

        public SaleItems updateSaleItems(SaleItems SaleItems)
        {
            var existing = _context.SaleItems.FirstOrDefault(si => si.SaleItemId == SaleItems.SaleItemId);
            if (existing == null)
                return null;

            existing.SaleId = SaleItems.SaleId;
            existing.ProductId = SaleItems.ProductId;
            existing.Quantity = SaleItems.Quantity;
            existing.Price = SaleItems.Price;
            // tambahkan properti lain jika ada

            _context.SaveChanges();
            return existing;
        }
    }
}
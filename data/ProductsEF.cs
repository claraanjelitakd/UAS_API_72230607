using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class ProductsEF : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductsEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public void deleteProducts(int ProductId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        IEnumerable<Products> IProduct.GetProducts()
        {
            return _context.Products.Include(p => p.Category)
                .OrderByDescending(p => p.ProductId)
                .ToList();
        }

        Products? IProduct.GetProductsById(int ProductId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == ProductId);
        }

        public Products addProducts(Products Products)
        {
            _context.Products.Add(Products);
            _context.SaveChanges();
            return Products;
        }

        public Products? updateProducts(Products Products)
        {
            var existing = _context.Products.FirstOrDefault(p => p.ProductId == Products.ProductId);
            if (existing == null)
                return null;

            existing.ProductName = Products.ProductName;
            existing.CategoryId = Products.CategoryId;
            existing.Price = Products.Price;
            existing.StockQuantity = Products.StockQuantity;
            existing.Description = Products.Description;

            _context.SaveChanges();
            return existing;
        }

        // Remove ViewProductCategories from interface and implementation if not needed
        // If needed, implement properly or remove from IProduct

        // Implement GetProductWithCategoryById as required by IProduct
        public ViewProductCategory? GetProductWithCategoryById(int productId)
        {
            var result = (from p in _context.Products
                          join c in _context.Categories on p.CategoryId equals c.CategoryID
                          where p.ProductId == productId
                          select new ViewProductCategory
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              Price = p.Price,
                              CategoryName = c.CategoryName
                          }).FirstOrDefault();

            return result;
        }

        public IEnumerable<ViewProductCategory> GetProductsWithCategories()
        {
            var result = from p in _context.Products
                         join c in _context.Categories on p.CategoryId equals c.CategoryID
                         select new ViewProductCategory
                         {
                             ProductId = p.ProductId,
                             ProductName = p.ProductName,
                             Price = p.Price,
                             CategoryName = c.CategoryName
                         };

            return result.ToList();
        }

        public Products ViewProductCategories()
        {
            throw new NotImplementedException();
        }
    }
}
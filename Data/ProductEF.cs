using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UAS_POS_CLARA.Models;
namespace UAS_POS_CLARA.Data
{
    public class ProductEF : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Addproduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("Error adding product", ex);
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = GetProductById(productId);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("Error deleting product", ex);
            }
        }

        public IEnumerable<Product> GetAllOfProduct()
        {
            var products = from c in _context.Products.Include(p => p.Category)
                           orderby c.ProductName descending
                           select c;

            return products;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var product = from c in _context.Products
                          orderby c.ProductName
                          select c;

            return product;
        }


        public Product GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var products = _context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryID == categoryId)
            .ToList();

            if (products == null || !products.Any())
            {
                throw new KeyNotFoundException("No products found for this category.");
            }
            return products;
        }

        public Product UpdateProduct(Product product)
        {
            var existingProduct = GetProductById(product.ProductID);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            try
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.CategoryID = product.CategoryID;
                existingProduct.Description = product.Description;
                existingProduct.Stock = product.Stock;
                _context.Products.Update(existingProduct);
                _context.SaveChanges();
                return existingProduct;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("Error updating product", ex);
            }
        }
    }
}


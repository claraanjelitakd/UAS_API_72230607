using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryEF : ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public Category addCategory(Category category)
        {
           try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding category: " + ex.Message);
            }
        }

        public void deleteCategory(int CategoryID)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == CategoryID);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category: " + ex.Message);
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            var category = _context.Categories.OrderByDescending(c => c.CategoryID).ToList();
            return category;
        }

        public Category GetCategoryById(int CategoryID)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == CategoryID);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public Category updateCategory(Category category)
        {
           var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
           if (existingCategory == null)
           {
               throw new Exception("Category not found");
           }
           try
           {
               existingCategory.CategoryName = category.CategoryName;
               _context.Categories.Update(existingCategory);
                _context.SaveChanges();
                return existingCategory;
            }      
            catch (Exception ex)
            {
                throw new Exception("Error updating category: " + ex.Message);
            }
        }
    }
}
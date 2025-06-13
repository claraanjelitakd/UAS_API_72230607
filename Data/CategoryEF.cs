using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public class CategoryEF : ICategory
    {
        private readonly ApplicationDbContext _context;

        public CategoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories
                           .OrderByDescending(c => c.CategoryName)
                           .ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _context.Categories
                                   .FirstOrDefault(c => c.CategoryID == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }
            return category;
        }

        public void DeleteCategory(int CategoryID)
        {
            var category = GetCategoryById(CategoryID);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("An error occurred while deleting the category.", ex);
            }
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void AddProductToCategory(int categoryId, Product product)
        {
            throw new NotImplementedException();
        }

        Category ICategory.AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("An error occurred while adding the category.", ex);
            }
        }

        Category ICategory.UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.CategoryID);
            if (existingCategory == null)
            {
            throw new KeyNotFoundException("Category not found.");
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
            // Handle exception (e.g., log it)
            throw new Exception("An error occurred while updating the category.", ex);
            }
        }
    }
}
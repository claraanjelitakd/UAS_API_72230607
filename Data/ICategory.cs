using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface ICategory
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int CategoryID);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(int CategoryID);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        void AddProductToCategory(int categoryId, Product product);
    }
}
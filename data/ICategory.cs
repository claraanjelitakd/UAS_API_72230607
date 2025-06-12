using System;
using SimpleRESTApi.Models;
namespace SimpleRESTApi.Data
{
    public interface ICategory
    {
        //crud
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int categoryID);
        Category addCategory(Category category);
        Category updateCategory(Category category);
        void deleteCategory(int categoryID);
    }
}
using System.Collections.Generic;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface IViewProductCategory
    {
        IEnumerable<ViewProductCategory> GetViewProductCategories();
        ViewProductCategory GetViewProductCategory(int categoryId); // Mengambil data berdasarkan ID di view
    }
}
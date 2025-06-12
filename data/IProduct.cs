using System;
using SimpleRESTApi.Models;
namespace SimpleRESTApi.Data
{
    public interface IProduct
    {
        //crud
        IEnumerable<Products> GetProducts();
        Products GetProductsById(int ProductId);
        Products addProducts(Products Products);
        Products updateProducts(Products Products);
        void deleteProducts(int ProductId);
        IEnumerable<ViewProductCategory> GetProductsWithCategories();
        ViewProductCategory GetProductWithCategoryById(int ProductId);
        Products ViewProductCategories();
        
    }
}
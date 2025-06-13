using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAS_POS_CLARA.Models;

namespace UAS_POS_CLARA.Data
{
    public interface IProduct
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int productId);
        Product Addproduct(Product product);
        Product UpdateProduct(Product product);
        void DeleteProduct(int productId);
        IEnumerable<Product> GetProductsByCategory(int CategoryID);
        IEnumerable<Product> GetAllOfProduct();

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class ProductsADO : IProduct
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;

        public ProductsADO(IConfiguration configuration) // Configuration from appsettings.json
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Products addProducts(Products product)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"INSERT INTO Products (ProductName, CategoryId, Price, StockQuantity, Description) 
                                  VALUES (@ProductName, @CategoryId, @Price, @StockQuantity, @Description); 
                                  SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    conn.Open();
                    int productId = Convert.ToInt32(cmd.ExecuteScalar());
                    product.ProductId = productId;
                    return product;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void deleteProducts(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"DELETE FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Product not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<Products> GetProducts()
        {
            List<Products> products = new List<Products>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT * FROM Products ORDER BY ProductId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Products product = new Products
                        {
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = dr["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32(dr["CategoryId"]),
                            Price = Convert.ToDecimal(dr["Price"]),
                            StockQuantity = Convert.ToInt32(dr["StockQuantity"]),
                            Description = dr["Description"].ToString()
                        };
                        products.Add(product);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return products;
        }

        public Products GetProductsById(int productId)
        {
            Products product = new Products();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT * FROM Products WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    product.ProductId = Convert.ToInt32(dr["ProductId"]);
                    product.ProductName = dr["ProductName"].ToString();
                    product.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    product.Price = Convert.ToDecimal(dr["Price"]);
                    product.StockQuantity = Convert.ToInt32(dr["StockQuantity"]);
                    product.Description = dr["Description"].ToString();
                }
                else
                {
                    throw new Exception("Product not found");
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return product;
        }

        public IEnumerable<ViewProductCategory> GetProductsWithCategories()
        {
            throw new NotImplementedException();
        }

        public ViewProductCategory GetProductWithCategoryById(int ProductId)
        {
            throw new NotImplementedException();
        }

        public Products updateProducts(Products product)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE Products 
                                  SET ProductName = @ProductName, CategoryId = @CategoryId, Price = @Price, 
                                      StockQuantity = @StockQuantity, Description = @Description 
                                  WHERE ProductId = @ProductId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Product not found");
                    }
                    return product;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public Products ViewProductCategories()
        {
            throw new NotImplementedException();
        }
    }
}
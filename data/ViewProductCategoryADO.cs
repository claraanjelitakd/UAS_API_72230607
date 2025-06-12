using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class ViewProductCategoryADO : IViewProductCategory
    {
        private readonly IConfiguration _configuration;
        private readonly string connStr;

        public ViewProductCategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public IEnumerable<ViewProductCategory> GetViewProductCategories()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT 
                                    dbo.Categories.CategoryID, 
                                    dbo.Categories.CategoryName, 
                                    dbo.Products.ProductId, 
                                    dbo.Products.ProductName, 
                                    dbo.Products.Price, 
                                    dbo.Products.StockQuantity, 
                                    dbo.Products.Description 
                                  FROM dbo.Categories 
                                  INNER JOIN dbo.Products ON dbo.Categories.CategoryID = dbo.Products.CategoryID";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                List<ViewProductCategory> productCategories = new List<ViewProductCategory>();
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ViewProductCategory viewProductCategory = new()
                            {
                                CategoryID = Convert.ToInt32(dr["CategoryID"]),
                                CategoryName = dr["CategoryName"].ToString(),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = dr["ProductName"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"]),
                                StockQuantity = Convert.ToInt32(dr["StockQuantity"]),
                                Description = dr["Description"].ToString()
                            };
                            productCategories.Add(viewProductCategory);
                        }
                    }
                    return productCategories;
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
        public ViewProductCategory GetViewProductCategory(int CategoryID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT 
                                    dbo.Categories.CategoryID, 
                                    dbo.Categories.CategoryName, 
                                    dbo.Products.ProductId, 
                                    dbo.Products.ProductName, 
                                    dbo.Products.Price, 
                                    dbo.Products.StockQuantity, 
                                    dbo.Products.Description 
                                  FROM dbo.Categories 
                                  INNER JOIN dbo.Products ON dbo.Categories.CategoryID = dbo.Products.CategoryID
                                  WHERE dbo.Categories.CategoryID = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(strsql, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    ViewProductCategory productCategory = new();
                    try
                    {
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                productCategory.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                                productCategory.CategoryName = dr["CategoryName"].ToString();
                                productCategory.ProductId = Convert.ToInt32(dr["ProductId"]);
                                productCategory.ProductName = dr["ProductName"].ToString();
                                productCategory.Price = Convert.ToDecimal(dr["Price"]);
                                productCategory.StockQuantity = Convert.ToInt32(dr["StockQuantity"]);
                                productCategory.Description = dr["Description"].ToString();
                            }
                        }
                        return productCategory;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}

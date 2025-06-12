using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class ViewSalesProductWithCustomersADO : IViewSalesProductWithCustomers
    {
        private readonly IConfiguration _configuration;
        private readonly string connStr;

        public ViewSalesProductWithCustomersADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ViewSalesProductWithCustomers> GetViewSalesProductWithCustomers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT dbo.Sales.SaleId, dbo.Sales.SaleDate, dbo.Sales.CustomerId, dbo.Sales.TotalAmount, dbo.Products.ProductId, dbo.Products.ProductName, dbo.Products.CategoryId, dbo.Products.Price, dbo.Customers.CustomerId AS Expr1, dbo.Customers.CustomerName, 
             dbo.Customers.ConctactNumber, dbo.Customers.Email, dbo.SaleItem.SaleItemId, dbo.SaleItem.Quantity
FROM   dbo.Sales INNER JOIN
             dbo.Customers ON dbo.Sales.CustomerId = dbo.Customers.CustomerId INNER JOIN
             dbo.SaleItem ON dbo.Sales.SaleId = dbo.SaleItem.SaleId INNER JOIN
             dbo.Products ON dbo.SaleItem.ProductId = dbo.Products.ProductId";
                using (SqlCommand cmd = new SqlCommand(strsql, conn))
                {
                    List<ViewSalesProductWithCustomers> salesList = new List<ViewSalesProductWithCustomers>();
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var sale = new ViewSalesProductWithCustomers
                                {
                                    SaleId = Convert.ToInt32(dr["SaleId"]),
                                    SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                                    CustomerId = Convert.ToInt32(dr["CustomerId"]),
                                    TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                                    ProductId = Convert.ToInt32(dr["ProductId"]),
                                    ProductName = dr["ProductName"].ToString(),
                                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
                                    Price = Convert.ToDecimal(dr["Price"]),
                                    StockQuantity = Convert.ToInt32(dr["stockQuantity"]),
                                    Description = dr["description"].ToString(),
                                    CustomerName = dr["CustomerName"].ToString(),
                                    ConctactNumber = dr["ConctactNumber"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Address = dr["address"].ToString(),
                                    SaleItemId = Convert.ToInt32(dr["SaleItemId"]),
                                    Quantity = Convert.ToInt32(dr["Quantity"])
                                };
                                salesList.Add(sale);
                            }
                        }
                        return salesList;
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
        }

        public ViewSalesProductWithCustomers GetViewSalesProductWithCustomers(int saleId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                        string strsql = @"SELECT dbo.Sales.SaleId, dbo.Sales.SaleDate, dbo.Sales.CustomerId, dbo.Sales.TotalAmount, dbo.Products.ProductId, dbo.Products.ProductName, dbo.Products.CategoryId, dbo.Products.Price, dbo.Customers.CustomerId AS Expr1, dbo.Customers.CustomerName, 
             dbo.Customers.ConctactNumber, dbo.Customers.Email, dbo.SaleItem.SaleItemId, dbo.SaleItem.Quantity
FROM   dbo.Sales INNER JOIN
             dbo.Customers ON dbo.Sales.CustomerId = dbo.Customers.CustomerId INNER JOIN
             dbo.SaleItem ON dbo.Sales.SaleId = dbo.SaleItem.SaleId INNER JOIN
             dbo.Products ON dbo.SaleItem.ProductId = dbo.Products.ProductId WHERE dbo.Sales.SaleId = @SaleId";
                using (SqlCommand cmd = new SqlCommand(strsql, conn))
                {
                    cmd.Parameters.AddWithValue("@SaleId", saleId);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                var sale = new ViewSalesProductWithCustomers
                                {
                                    SaleId = Convert.ToInt32(dr["SaleId"]),
                                    SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                                    CustomerId = Convert.ToInt32(dr["CustomerId"]),
                                    TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                                    ProductId = Convert.ToInt32(dr["ProductId"]),
                                    ProductName = dr["ProductName"].ToString(),
                                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
                                    Price = Convert.ToDecimal(dr["Price"]),
                                    StockQuantity = Convert.ToInt32(dr["stockQuantity"]),
                                    Description = dr["description"].ToString(),
                                    CustomerName = dr["CustomerName"].ToString(),
                                    ConctactNumber = dr["ConctactNumber"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Address = dr["address"].ToString(),
                                    SaleItemId = Convert.ToInt32(dr["SaleItemId"]),
                                    Quantity = Convert.ToInt32(dr["Quantity"])
                                };
                                return sale;
                            }
                            else
                            {
                                return null;
                            }
                        }
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

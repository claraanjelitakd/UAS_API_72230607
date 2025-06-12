using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class SalesADO : ISales
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;

        public SalesADO(IConfiguration configuration) // Configuration from appsettings.json
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Sales addSales(Sales sales)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"INSERT INTO Sales (CustomerId, SaleDate, TotalAmount) 
                                  VALUES (@CustomerId, @SaleDate, @TotalAmount); 
                                  SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerId", sales.CustomerId);
                    cmd.Parameters.AddWithValue("@SaleDate", sales.SaleDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", sales.TotalAmount);
                    conn.Open();
                    sales.SaleId = Convert.ToInt32(cmd.ExecuteScalar());
                    return sales;
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

        public void deleteSales(int salesId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "DELETE FROM Sales WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@SaleId", salesId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Sale not found");
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

        public IEnumerable<Sales> GetSales()
        {
            List<Sales> salesList = new List<Sales>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "SELECT * FROM Sales ORDER BY SaleId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Sales sales = new Sales
                        {
                            SaleId = Convert.ToInt32(dr["SaleId"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            SaleDate = Convert.ToDateTime(dr["SaleDate"]),
                            TotalAmount = Convert.ToDecimal(dr["TotalAmount"])
                        };
                        salesList.Add(sales);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return salesList;
        }

        public Sales GetSalesById(int salesId)
        {
            Sales sales = new Sales();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "SELECT * FROM Sales WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@SaleId", salesId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    sales.SaleId = Convert.ToInt32(dr["SaleId"]);
                    sales.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                    sales.SaleDate = Convert.ToDateTime(dr["SaleDate"]);
                    sales.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                }
                else
                {
                    throw new Exception("Sale not found");
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return sales;
        }

        public IEnumerable<SaleDetails> GetSalesWithDetails()
        {
            throw new NotImplementedException();
        }

        public SaleDetails GetSaleWithDetailsById(int SaleId)
        {
            throw new NotImplementedException();
        }

        public Sales updateSales(Sales sales)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE Sales 
                                  SET CustomerId = @CustomerId, SaleDate = @SaleDate, TotalAmount = @TotalAmount 
                                  WHERE SaleId = @SaleId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerId", sales.CustomerId);
                    cmd.Parameters.AddWithValue("@SaleDate", sales.SaleDate);
                    cmd.Parameters.AddWithValue("@TotalAmount", sales.TotalAmount);
                    cmd.Parameters.AddWithValue("@SaleId", sales.SaleId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Sale not found");
                    }
                    return sales;
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
}

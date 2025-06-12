using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class SaleItemsADO : ISaleItems
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;

        public SaleItemsADO(IConfiguration configuration) // Configuration from appsettings.json
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public SaleItems addSaleItems(SaleItems saleItems)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"INSERT INTO SaleItem(SaleId, ProductId, Quantity, Price) 
                                  VALUES (@SaleId, @ProductId, @Quantity, @Price); 
                                  SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@SaleId", saleItems.SaleId);
                    cmd.Parameters.AddWithValue("@ProductId", saleItems.ProductId);
                    cmd.Parameters.AddWithValue("@Quantity", saleItems.Quantity);
                    cmd.Parameters.AddWithValue("@Price", saleItems.Price);
                    conn.Open();
                    saleItems.SaleItemId = Convert.ToInt32(cmd.ExecuteScalar());
                    return saleItems;
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

        public void deleteSaleItems(int saleItemID)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "DELETE FROM SaleItem WHERE SaleItemId = @SaleItemId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@SaleItemId", saleItemID);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Sale item not found");
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

        public IEnumerable<SaleItems> GetSaleItems()
        {
            List<SaleItems> saleItemsList = new List<SaleItems>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "SELECT * FROM SaleItem ORDER BY SaleItemId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SaleItems saleItem = new SaleItems
                        {
                            SaleItemId = Convert.ToInt32(dr["SaleItemId"]),
                            SaleId = Convert.ToInt32(dr["SaleId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            Quantity = Convert.ToInt32(dr["Quantity"]),
                            Price = Convert.ToDecimal(dr["Price"])
                        };
                        saleItemsList.Add(saleItem);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return saleItemsList;
        }

        public SaleItems GetSaleItemsById(int saleItemID)
        {
            SaleItems saleItem = new SaleItems();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "SELECT * FROM SaleItem WHERE SaleItemId = @SaleItemId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@SaleItemId", saleItemID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    saleItem.SaleItemId = Convert.ToInt32(dr["SaleItemId"]);
                    saleItem.SaleId = Convert.ToInt32(dr["SaleId"]);
                    saleItem.ProductId = Convert.ToInt32(dr["ProductId"]);
                    saleItem.Quantity = Convert.ToInt32(dr["Quantity"]);
                    saleItem.Price = Convert.ToDecimal(dr["Price"]);
                }
                else
                {
                    throw new Exception("Sale item not found");
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return saleItem;
        }

        public SaleItems updateSaleItems(SaleItems saleItems)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE SaleItem 
                                  SET SaleId = @SaleId, ProductId = @ProductId, Quantity = @Quantity, Price = @Price 
                                  WHERE SaleItemId = @SaleItemId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@SaleId", saleItems.SaleId);
                    cmd.Parameters.AddWithValue("@ProductId", saleItems.ProductId);
                    cmd.Parameters.AddWithValue("@Quantity", saleItems.Quantity);
                    cmd.Parameters.AddWithValue("@Price", saleItems.Price);
                    cmd.Parameters.AddWithValue("@SaleItemId", saleItems.SaleItemId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Sale item not found");
                    }
                    return saleItems;
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

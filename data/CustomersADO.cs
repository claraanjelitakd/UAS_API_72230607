using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CustomersADO : ICustomers
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;

        public CustomersADO(IConfiguration configuration) // Configuration from appsettings.json
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Customers addCustomers(Customers customer)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"INSERT INTO Customers (CustomerName, ConctactNumber, Email, Address) 
                                  VALUES (@CustomerName, @ConctactNumber, @Email, @Address); 
                                  SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@ConctactNumber", customer.ConctactNumber);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    conn.Open();
                    int customerId = Convert.ToInt32(cmd.ExecuteScalar());
                    customer.CustomerId = customerId;
                    return customer;
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

        public void deleteCustomers(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"DELETE FROM Customers WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Customer not found");
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

        public IEnumerable<Customers> GetCustomers()
        {
            List<Customers> customers = new List<Customers>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT * FROM Customers ORDER BY CustomerId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Customers customer = new Customers
                        {
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerName = dr["CustomerName"].ToString(),
                            ConctactNumber = dr["ConctactNumber".Trim()].ToString(),
                            Email = dr["Email"].ToString(),
                            Address = dr["Address"].ToString()
                        };
                        customers.Add(customer);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return customers;
        }

        public Customers GetCustomersById(int customerId)
        {
            Customers customer = new Customers();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT * FROM Customers WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    customer.CustomerId = Convert.ToInt32(dr["CustomerId"]);
                    customer.CustomerName = dr["CustomerName"].ToString();
                    customer.ConctactNumber = dr["ConctactNumber"].ToString();
                    customer.Email = dr["Email"].ToString();
                    customer.Address = dr["Address"].ToString();
                }
                else
                {
                    throw new Exception("Customer not found");
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return customer;
        }

        public Customers updateCustomers(Customers customer)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE Customers 
                                  SET CustomerName = @CustomerName, ConctactNumber = @ConctactNumber, 
                                      Email = @Email, Address = @Address 
                                  WHERE CustomerId = @CustomerId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@ConctactNumber", customer.ConctactNumber);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Customer not found");
                    }
                    return customer;
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
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class EmployeesADO : IEmployees
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;

        public EmployeesADO(IConfiguration configuration) // Configuration from appsettings.json
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Employees AddEmployees(Employees employees)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"INSERT INTO Employees (EmployeeName, ContactNumber, Email, Position) 
                                  VALUES (@EmployeeName, @ContactNumber, @Email, @Position); 
                                  SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", employees.EmployeeName);
                    cmd.Parameters.AddWithValue("@ContactNumber", employees.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", employees.Email);
                    cmd.Parameters.AddWithValue("@Position", employees.Position);
                    conn.Open();
                    employees.EmployeeId = Convert.ToInt32(cmd.ExecuteScalar());
                    return employees;
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

        public void DeleteEmployees(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Employee not found");
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

        public IEnumerable<Employees> GetEmployees()
        {
            List<Employees> employeesList = new List<Employees>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "SELECT * FROM Employees ORDER BY EmployeeId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Employees employee = new Employees
                        {
                            EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                            EmployeeName = dr["EmployeeName"].ToString(),
                            ContactNumber = dr["ContactNumber"].ToString(),
                            Email = dr["Email"].ToString(),
                            Position = dr["Position"].ToString()
                        };
                        employeesList.Add(employee);
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return employeesList;
        }

        public Employees GetEmployeesById(int employeeId)
        {
            Employees employee = new Employees();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    employee.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                    employee.EmployeeName = dr["EmployeeName"].ToString();
                    employee.ContactNumber = dr["ContactNumber"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.Position = dr["Position"].ToString();
                }
                else
                {
                    throw new Exception("Employee not found");
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return employee;
        }

        public Employees UpdateEmployees(Employees employees)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE Employees 
                                  SET EmployeeName = @EmployeeName, ContactNumber = @ContactNumber, Email = @Email, Position = @Position 
                                  WHERE EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", employees.EmployeeName);
                    cmd.Parameters.AddWithValue("@ContactNumber", employees.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", employees.Email);
                    cmd.Parameters.AddWithValue("@Position", employees.Position);
                    cmd.Parameters.AddWithValue("@EmployeeId", employees.EmployeeId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        throw new Exception("Employee not found");
                    }
                    return employees;
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

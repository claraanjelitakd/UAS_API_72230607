using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryADO : ICategory
    {
        private IConfiguration _configuration;
        private string connStr = string.Empty;

        public CategoryADO(IConfiguration configuration) //configurasi dari appsettings.json
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public Category addCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
              string strsql = @"INSERT INTO categories (CategoryName) VALUES (@CategoryName); SELECT SCOPE_IDENTITY()"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
              SqlCommand cmd = new SqlCommand(strsql, conn);
              try
              {

                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    conn.Open();
                    int CategoryID = Convert.ToInt32(cmd.ExecuteScalar());
                    category.CategoryID = CategoryID;
                    return category;

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

        public void deleteCategory(int CategoryID)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
            string strsql = @"DELETE FROM categories WHERE CategoryID = @CategoryID"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
            SqlCommand cmd = new SqlCommand(strsql, conn);
            try{
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result==0)
                {
                    throw new Exception("Category not found");
                }
            }
            catch(Exception ex)
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

        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>(); //diluar using
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT*FROM categories ORDER BY CategoryID"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(); //baca data pakai data reader trs dimaping make while
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        //dimaping di class
                        Category category = new();
                        category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                        category.CategoryName = dr["CategoryName"].ToString();
                        categories.Add(category); //di add karena datanya lebih dari 1
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close(); // sebenernya gausa di close gapapa soalnya di end of scope krn make using ini otomatis di close konseksinya.

            }
            return categories;

        }

        public Category GetCategoryById(int CategoryID)
        {
            Category category = new();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"SELECT*FROM categories WHERE CategoryID = @category"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
                //jangan pakai string biasa untuk menghindari sql injeksion di sanitize dulu
                SqlCommand cmd = new SqlCommand(strsql, conn);
                cmd.Parameters.AddWithValue("@category", CategoryID);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(); //baca data pakai data reader trs dimaping make while
                if(dr.HasRows)
                {
                    dr.Read();
                    //dimaping di class
                    
                    category.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                }      
                else
                {throw new Exception("Category not found");}
            }
            return category;
        }

        public Category updateCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strsql = @"UPDATE categories SET CategoryName = @CategoryName
                WHERE CategoryID = @CategoryID"; //mengambil data dari tabel --> membuat urut dari ID bukan dari name
                SqlCommand cmd = new SqlCommand(strsql, conn);
                try{
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result==0)
                    {
                        throw new Exception("Category not found");
                    }
                    return category;
                }
                catch(Exception ex)
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
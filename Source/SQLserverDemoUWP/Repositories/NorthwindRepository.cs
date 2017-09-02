using SQLserverDemoUWP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SQLserverDemoUWP.Repositories
{
    public class NorthwindRepository
    {
        private readonly string connectionString =
           @"Server=localhost\SQLEXPRESS;Database=Northwind; User Id=MyNewAdminUser; Password=abcd";

        public async Task<List<Product>> SearchProducts(string query)
        {
            return await Task.Run<List<Product>>(() =>
            {
                List<Product> result = new List<Product>();
                string queryString =
                          "SELECT ProductName, UnitPrice, QuantityPerUnit "
                        + "FROM dbo.products "
                        + "WHERE ProductName like '%' + @query + '%'"
                        + "ORDER BY ProductName DESC;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@query", query);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Product
                            {
                                ProductName = reader.GetString(0),
                                UnitPrice = reader.GetDecimal(1),
                                QuantityPerUnit = reader.GetString(2)
                            });
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return result;
                }
            });
        }
    }
}

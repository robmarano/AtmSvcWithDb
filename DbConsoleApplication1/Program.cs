using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DbConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = "Data Source=(local);Initial Catalog=AdventureWorks2012;Integrated Security=true";
            string connectionString = "Data Source=(local);Initial Catalog=AtmDb;Integrated Security=true";

            string username = "rob";
            string password = "hello";

            string insertString = "INSERT INTO [AtmDb].[dbo].[Credentials] (username,password) VALUES (@username,@password)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(insertString, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    int numRowsInserted = command.ExecuteNonQuery();
                    if (numRowsInserted > 0)
                    {
                        Console.WriteLine("Wrote " + numRowsInserted + " to database");
                    }
                }
                catch (SqlException e)
                {
                    if (e.Number == 2601) // Cannot insert duplicate key row in object error
                    {
                        Console.WriteLine("Duplicate entry.");
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.ToString());
                }
                connection.Close();
            }

            // Provide the query string with a parameter placeholder.
            string queryString = "SELECT uid, username, password FROM [AtmDb].[dbo].[Credentials] ORDER BY uid DESC";

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                        reader[0], reader[1], reader[2]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.ToString());
                }
                connection.Close();
            }
        }
    }
}

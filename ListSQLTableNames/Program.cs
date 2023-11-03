using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Replace with your server name, database name, and access credentials
        string connectionString = "Server=YOUR_SERVER_NAME; Database=YOUR_DATABASE_NAME; User Id=YOUR_USERNAME; Password=YOUR_PASSWORD;";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // SQL query to select all table names with their schema
                string sqlQuery = @"SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_SCHEMA, TABLE_NAME;";

                // Create a SqlCommand object
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Execute the command and receive the data in a SqlDataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Schema\t\tTable Name");
                        Console.WriteLine("-------------------------------------");

                        // Iterate through all the rows in the result set
                        while (reader.Read())
                        {
                            string schema = reader.GetString(0); // TABLE_SCHEMA
                            string tableName = reader.GetString(1); // TABLE_NAME

                            // Print the schema and table name
                            Console.WriteLine($"{schema}\t\t{tableName}");
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            // Handle any SQL related errors
            Console.WriteLine("A SQL error occurred: " + e.Message);
        }
        catch (Exception e)
        {
            // Handle any other errors
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Replace with your server name, database name, and access credentials
        string connectionString = "Server=YOUR_SERVER_NAME; Database=YOUR_DATABASE_NAME; User Id=YOUR_USERNAME; Password=YOUR_PASSWORD;";

        Console.WriteLine("Select an option:");
        Console.WriteLine("1. List Tables");
        Console.WriteLine("2. List Views");
        Console.WriteLine("3. List Stored Procedures");
        Console.WriteLine("4. Exit");

        Console.Write("Enter your option (1-4): ");
        string option = Console.ReadLine();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                switch (option)
                {
                    case "1":
                        ListTables(connection);
                        break;
                    case "2":
                        ListViews(connection);
                        break;
                    case "3":
                        ListStoredProcedures(connection);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine("A SQL error occurred: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    }

    static void ListTables(SqlConnection connection)
    {
        string sqlQuery = @"SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_SCHEMA, TABLE_NAME;";
        ExecuteAndDisplayQuery(connection, sqlQuery);
    }

    static void ListViews(SqlConnection connection)
    {
        string sqlQuery = @"SELECT TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS ORDER BY TABLE_SCHEMA, TABLE_NAME;";
        ExecuteAndDisplayQuery(connection, sqlQuery);
    }

    static void ListStoredProcedures(SqlConnection connection)
    {
        string sqlQuery = @"SELECT SPECIFIC_SCHEMA, SPECIFIC_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' ORDER BY SPECIFIC_SCHEMA, SPECIFIC_NAME;";
        ExecuteAndDisplayQuery(connection, sqlQuery);
    }

    static void ExecuteAndDisplayQuery(SqlConnection connection, string sqlQuery)
    {
        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Schema\t\tName");
                Console.WriteLine("-------------------------------------");

                while (reader.Read())
                {
                    string schema = reader.GetString(0);
                    string name = reader.GetString(1);
                    Console.WriteLine($"{schema}\t\t{name}");
                }
            }
        }
    }
}

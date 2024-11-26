namespace FinanceApp;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        var handler = new DatabaseHandler();
        
        // string connectionString = "Host=localhost;Username=postgres;Password=password;Database=finance_app";

        // using var connection = new NpgsqlConnection(connectionString);

        // connection.Open();

        // var sql = "CREATE TABLE IF NOT EXISTS accounts (id UUID PRIMARY KEY, user_name TEXT NOT NULL, balance DECIMAL CHECK(balance <= 0)";

        // using var cmd = new NpgsqlCommand(sql, connection);

        // cmd.ExecuteNonQuery();
    }
}
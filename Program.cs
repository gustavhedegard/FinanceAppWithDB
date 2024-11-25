namespace FinanceApp;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=finance_app";

        using var connection = new NpgsqlConnection(connectionString);

        connection.Open();

        var sql = "CREATE TABLE IF NOT EXISTS accounts (id UUID PRIMARY KEY, name TEXT NOT NULL)";

        using var cmd = new NpgsqlCommand(sql, connection);

        cmd.ExecuteNonQuery();
    }
}
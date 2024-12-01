namespace FinanceApp;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=finance_app";

        var npgsqlConnection = new NpgsqlConnection(connectionString);

        npgsqlConnection.Open();

        var createTablesQuery = @"
            CREATE TABLE IF NOT EXISTS users (
            account_id UUID PRIMARY KEY
            name TEXT NOT NULL,
            balance DECIMAL CHECK(balance <= 0)
        );
            
            CREATE TABLE IF NOT EXISTS transactions (
            id UUID PRIMARY KEY,
            account_id UUID REFERENCES accounts(account_id),
            amount DECIMAL NOT NULL,
            date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )";

        using var executeSqlCmd = new NpgsqlCommand(createTablesQuery, npgsqlConnection); 
        executeSqlCmd.ExecuteNonQuery();
    }
}
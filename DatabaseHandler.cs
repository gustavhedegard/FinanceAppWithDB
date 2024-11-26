using Npgsql;
public class DatabaseHandler {

    private NpgsqlConnection npgsqlConnection;

    public DatabaseHandler() {

        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=finance_app";

        this.npgsqlConnection = new NpgsqlConnection(connectionString);

        npgsqlConnection.Open();

        ExecuteSqlCmd(CreateAccountTableQuery());
        ExecuteSqlCmd(CreateTransactionTableQuery());
    }

    public void ExecuteSqlCmd(string query) {

        using var executeSqlCmd = new NpgsqlCommand(query, npgsqlConnection); 
        executeSqlCmd.ExecuteNonQuery();
    }

    public string CreateAccountTableQuery() {
        
        return @"CREATE TABLE IF NOT EXISTS accounts (
            user_name TEXT PRIMARY KEY,
            balance DECIMAL CHECK(balance <= 0)
        )";
    }

    public string CreateTransactionTableQuery() {

        return @"CREATE TABLE IF NOT EXISTS transactions (
            id UUID PRIMARY KEY,
            user TEXT REFERENCES accounts(user_name),
            amount DECIMAL NOT NULL,
            date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )";
    }

    public void Close() {
        npgsqlConnection.Close();
    }
}
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
            account_id UUID PRIMARY KEY
            name TEXT NOT NULL,
            balance DECIMAL CHECK(balance <= 0)
        )";
    }

    public string CreateTransactionTableQuery() {

        return @"CREATE TABLE IF NOT EXISTS transactions (
            id UUID PRIMARY KEY,
            account_id UUID REFERENCES accounts(account_id),
            amount DECIMAL NOT NULL,
            date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )";
    }

    public void InsertDepositSql(double amount) {
        var insertDepositSql = @"INSERT INTO transactions (amount) VALUES (@amount)";

        using (var insertDepositCmd = new NpgsqlCommand(insertDepositSql, npgsqlConnection)) {
            insertDepositCmd.Parameters.AddWithValue("@amount", amount);

            insertDepositCmd.ExecuteNonQuery();
        }
    }

    public void Close() {
        npgsqlConnection.Close();
    }
}
namespace FinanceApp;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=finance_app";

        using var npgsqlConnection = new NpgsqlConnection(connectionString);

        npgsqlConnection.Open();

        var createTablesQuery = @"
            CREATE TABLE IF NOT EXISTS users (
            id UUID PRIMARY KEY,
            name TEXT NOT NULL,
            password TEXT NOT NULL,
            balance DECIMAL CHECK(balance >= 0)
        );
            
            CREATE TABLE IF NOT EXISTS transactions (
            id UUID PRIMARY KEY,
            user_id UUID REFERENCES users(id),
            amount DECIMAL NOT NULL,
            type TEXT NOT NULL,
            date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        )";

        using var executeSqlCmd = new NpgsqlCommand(createTablesQuery, npgsqlConnection); 
        executeSqlCmd.ExecuteNonQuery();

        var userService = new PostgresUserService(npgsqlConnection);

        LoginMenu loginMenu = new LoginMenu(userService);
        loginMenu.Display();
        // var loggedInUser = new User() {
        //     Name = "gustav",
        //     Password = "123"
        // };
        var loggedInUser = userService.GetLoggedInUser();

        Console.WriteLine(loggedInUser.Id);
        var transactionService = new PostgresTransactionService(npgsqlConnection, userService);

        transactionService.SaveTransaction(400, "withdrawal");
    }
}
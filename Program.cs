namespace FinanceApp;
using Npgsql;

class Program
{

    private PostgresUserService postgresUserService;
    static void Main(string[] args)
    {
        
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=finance_app";

        var npgsqlConnection = new NpgsqlConnection(connectionString);

        npgsqlConnection.Open();

        var createTablesQuery = @"
            CREATE TABLE IF NOT EXISTS users (
            id UUID PRIMARY KEY,
            name TEXT NOT NULL,
            password TEXT NOT NULL,
            balance DECIMAL CHECK(balance <= 0)
        );
            
            CREATE TABLE IF NOT EXISTS transactions (
            id UUID PRIMARY KEY,
            user_id UUID REFERENCES users(id),
            amount DECIMAL NOT NULL,
            date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
            type TEXT NOT NULL
        )";

        using var executeSqlCmd = new NpgsqlCommand(createTablesQuery, npgsqlConnection); 
        executeSqlCmd.ExecuteNonQuery();

        var userService = new PostgresUserService(npgsqlConnection);

        //userService.RegisterUser("Gustav", "123");

        User? user = userService.Login("Gustav", "123");

        if (user != null) {
            Console.WriteLine(user.Id);
        }
        else {
            Console.WriteLine("Wrong name or password.");
        }


    }
}
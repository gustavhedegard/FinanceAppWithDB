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

        IUserService userService = new PostgresUserService(npgsqlConnection);
        IUtilitiesService utilitiesService = new UtilitiesService(npgsqlConnection, userService);
        ITransactionService transactionService = new PostgresTransactionService(npgsqlConnection, utilitiesService);
        IMenuService menuService = new MenuService();

        var loginMenu = new LoginMenu(userService, menuService, transactionService, utilitiesService);
        menuService.SetMenu(loginMenu);
        var userMenu = new UserMenu(userService, menuService, transactionService, utilitiesService);

        while(true) {
            string? inputCommand = Console.ReadLine();
            if (inputCommand != null) {
                try {
                    menuService.GetMenu().ExecuteCommand(inputCommand);
                } catch {
                    Console.WriteLine("Please enter a valid input");
                    utilitiesService.PressKeyToContinue();
                    menuService.SetMenu(userMenu); 
                }
                
            } else {
                break;
            }
        }
    }
}
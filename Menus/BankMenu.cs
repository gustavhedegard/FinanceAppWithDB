using FinanceApp;

public class BankMenu {
    private PostgresTransactionService _postgresTransactionService;

    public BankMenu(PostgresTransactionService postgresTransactionService) {
        _postgresTransactionService = postgresTransactionService;
    }

    public void Display(PostgresUserService _postgresUserService) {
        var loggedInUser = _postgresUserService.GetLoggedInUser();
        
        while (loggedInUser != null) {
            Console.WriteLine($"Welcome {loggedInUser.Name}!");
            Console.WriteLine("Choose an option:\n1 - Check your balance\n2 - Deposit\n3 - Withdraw\n4 - Search\n5 - Remove transaction\n6 - Exit");
            try {

                int number = int.Parse(Console.ReadLine()!);
                
                switch (number) {
                    case 1:
                        double balance =_postgresTransactionService.GetBalance();
                        Console.WriteLine("Your balance is " + balance);

                        break;

                    case 2:
                        
                        break;
                    
                    case 3:
                        
                        break;
                    
                    case 4:
                        
                        break;
                    
                    case 5:
                        
                        break;
                    
                    case 6:
                        Console.WriteLine("Goodbye!");
                        _postgresUserService.Logout();
                        loggedInUser = _postgresUserService.GetLoggedInUser();
                        break;
                    
                    default:
                        Console.WriteLine("Choose a valid number");
                        break;
                }
            }
            catch (FormatException exception) {
                Console.WriteLine("Enter a number : " + exception.Message);      
            }

            Program.Main();
        }     
    }
    
}
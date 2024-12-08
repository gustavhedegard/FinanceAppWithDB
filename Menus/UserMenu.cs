using FinanceApp;

public class UserMenu : Menu {


    public UserMenu( IUserService userService, IMenuService menuService, ITransactionService transactionService) {
    }

    public override void Display() {
        Console.WriteLine($"Welcome to the usermenu!");
    }
        
}
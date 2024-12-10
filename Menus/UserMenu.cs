using FinanceApp;

public class UserMenu : Menu {
    public UserMenu( IUserService userService, IMenuService menuService, ITransactionService transactionService) {
        AddCommand(new LogoutCommand(userService, transactionService, menuService));
        AddCommand(new CheckBalanceCommand(userService, transactionService, menuService));
        AddCommand(new MakeTransactionCommand(userService, transactionService, menuService));
        AddCommand(new RemoveTransactionCommand(userService, transactionService,menuService));
    }

    public override void Display() {
        Console.WriteLine($"Welcome!");
    }
        
}
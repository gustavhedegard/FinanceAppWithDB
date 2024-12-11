using FinanceApp;

public class UserMenu : Menu {
    private IUserService _userService;
    public UserMenu( IUserService userService, IMenuService menuService, ITransactionService transactionService) {
        AddCommand(new LogoutCommand(userService, transactionService, menuService));
        AddCommand(new CheckBalanceCommand(userService, transactionService, menuService));
        AddCommand(new WithdrawCommand(userService, transactionService, menuService));
        AddCommand(new RemoveTransactionCommand(userService, transactionService,menuService));
        AddCommand(new DepositCommand(userService, transactionService, menuService));
    }

    public override void Display() {

        var user = _userService.GetLoggedInUser();
        Console.WriteLine($"Welcome!");
    }
        
}
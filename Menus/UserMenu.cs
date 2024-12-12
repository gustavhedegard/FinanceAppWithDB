using FinanceApp;

public class UserMenu : Menu {
    private IUserService _userService;
    public UserMenu( IUserService userService, IMenuService menuService, ITransactionService transactionService) {
        _userService = userService;
        AddCommand(new LogoutCommand(userService, transactionService, menuService));
        AddCommand(new CheckBalanceCommand(userService, transactionService, menuService));
        AddCommand(new WithdrawCommand(userService, transactionService, menuService));
        AddCommand(new RemoveTransactionCommand(userService, transactionService,menuService));
        AddCommand(new DepositCommand(userService, transactionService, menuService));
        AddCommand(new SearchByYearCommand(userService, transactionService, menuService));
        AddCommand(new SearchByMonthCommand(userService, transactionService, menuService));
        AddCommand(new SearchByWeekCommand(userService, transactionService, menuService));
        AddCommand(new SearchByDayCommand(userService, transactionService, menuService));
        AddCommand(new HelpCommand(userService, transactionService, menuService));
    }

    public override void Display() {

        var user = _userService.GetLoggedInUser();
        if(user == null) {
            throw new ArgumentException("You are not logged in."); 
        }

        Console.WriteLine($"Welcome {user.Name}!");
    }
        
}
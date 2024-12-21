using FinanceApp;

public class UserMenu : Menu {
    private IUserService _userService;
    public UserMenu( IUserService userService, IMenuService menuService, ITransactionService transactionService, IUtilitiesService utilitiesService) {
        _userService = userService;
        AddCommand(new LogoutCommand(userService, transactionService, menuService, utilitiesService));
        AddCommand(new CheckBalanceCommand(transactionService, utilitiesService));
        AddCommand(new WithdrawCommand(transactionService, utilitiesService));
        AddCommand(new RemoveTransactionCommand(transactionService,utilitiesService));
        AddCommand(new DepositCommand(transactionService, utilitiesService));
        AddCommand(new SearchByYearCommand(transactionService, utilitiesService));
        AddCommand(new SearchByMonthCommand(transactionService, utilitiesService));
        AddCommand(new SearchByWeekCommand(transactionService, utilitiesService));
        AddCommand(new SearchByDayCommand(transactionService, utilitiesService));
    }

    public override void Display() {

        var user = _userService.GetLoggedInUser();
        if(user == null) {
            throw new ArgumentException("You are not logged in."); 
        }

        Console.WriteLine($"Welcome {user.Name}!");
        Console.WriteLine("Please enter one of the following commands:");
        Console.WriteLine("check-balance");
        Console.WriteLine("withdraw <amount>");
        Console.WriteLine("deposit <amount>");
        Console.WriteLine("remove");
        Console.WriteLine("search-year <year>");
        Console.WriteLine("search-month <year> <month>");
        Console.WriteLine("search-week <year> <week>");
        Console.WriteLine("search-day <YYYYMMDD>");
        Console.WriteLine("logout");
        Console.WriteLine("--------------------------------------------------------------------------------");
    }
        
}
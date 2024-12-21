public class LoginMenu : Menu {
    public LoginMenu(IUserService userService, IMenuService menuService, ITransactionService transactionService, IUtilitiesService utilitiesService) {
        AddCommand(new LoginCommand(userService, transactionService, menuService, utilitiesService));
        AddCommand(new RegisterUserCommand(userService, transactionService, menuService, utilitiesService));
        AddCommand(new QuitCommand(userService, transactionService, menuService, utilitiesService));
    }

    public override void Display() {
        Console.WriteLine("Welcome to The Bank!");
        Console.WriteLine("Please type one of the following commands:");
        Console.WriteLine("'login <name> <password>'");
        Console.WriteLine("'register-user <name> <password>'");
        Console.WriteLine("'quit'");
        Console.WriteLine("-------------------------------------------------------------------------------");
    }
}
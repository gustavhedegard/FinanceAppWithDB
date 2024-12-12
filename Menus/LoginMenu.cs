public class LoginMenu : Menu {
    public LoginMenu(IUserService userService, IMenuService menuService, ITransactionService transactionService) {
        AddCommand(new LoginCommand(userService, transactionService, menuService));
        AddCommand(new RegisterUserCommand(userService, transactionService, menuService));
        AddCommand(new QuitCommand(userService, transactionService, menuService));
    }

    public override void Display() {
        Console.WriteLine("Welcome to The Bank!\nPlease type 'login' or 'register-user'.");
    }
}
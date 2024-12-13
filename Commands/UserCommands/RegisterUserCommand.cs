public class RegisterUserCommand : Command {
    public RegisterUserCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("register-user","register with name and password", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        string name = args[1];
        string password = args[2];

        User user = userService.RegisterUser(name, password);

        Console.WriteLine($"Created user '{user.Name}'");

    }
}
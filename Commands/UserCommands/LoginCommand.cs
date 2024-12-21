public class LoginCommand : Command {

    public LoginCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService, IUtilitiesService utilitiesService) : base("login", userService, transactionService, menuService, utilitiesService) {

    }

    public override void Execute(string[] args) {

        string name = args[1];
        string description = args[2];

        User? user = userService.Login(name, description);
        if (user == null) {
            Console.WriteLine("Wrong name or password");
            return;
        }
        
            Console.WriteLine("Login succeeded,");
            utilitiesService.PressKeyToContinue();
            menuService.SetMenu(new UserMenu(userService,menuService,transactionService, utilitiesService));

    }
}
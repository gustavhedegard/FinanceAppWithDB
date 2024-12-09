public class LogoutCommand : Command {

    public LogoutCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("logout","Log out current user", userService, transactionService, menuService) {

     }

    public override void Execute(string[] args)
    {
        userService.Logout();
        menuService.SetMenu(new LoginMenu(userService,menuService,transactionService));

    }

}
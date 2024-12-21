public class LogoutCommand : Command {

    public LogoutCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService, IUtilitiesService utilitiesService) : base("logout",userService, transactionService, menuService, utilitiesService) {

     }

    public override void Execute(string[] args)
    {
        userService.Logout();
        menuService.SetMenu(new LoginMenu(userService,menuService,transactionService,utilitiesService));

    }

}
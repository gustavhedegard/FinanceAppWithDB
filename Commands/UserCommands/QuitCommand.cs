public class QuitCommand : Command {

    public QuitCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService, IUtilitiesService utilitiesService) : base("quit", userService, transactionService, menuService, utilitiesService) {

     }

    public override void Execute(string[] args)
    {
        Environment.Exit(0);

    }

}
public class QuitCommand : Command {

    public QuitCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("quit","quit the application", userService, transactionService, menuService) {

     }

    public override void Execute(string[] args)
    {
        Environment.Exit(0);

    }

}
public class HelpCommand : Command {

    public HelpCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("help","See a list of possible actions", userService, transactionService, menuService) {
        
    }

    public override void Execute(string[] args)
    {
       
    }
}
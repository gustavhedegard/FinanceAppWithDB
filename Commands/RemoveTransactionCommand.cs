public class RemoveTransactionCommand : Command {
    public RemoveTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("remove","Remove a transaction", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        Guid id = Guid.Parse(args[1]);

        transactionService.RemoveTransaction(id);
    }
}
public class MakeTransactionCommand : Command {

    public MakeTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("make-transaction","Make a withdrawal or deposit", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        string type = args[1];
        double amount = Convert.ToDouble(args[2]);

        Transaction transaction = transactionService.ExecuteTransaction(type, amount);
        Console.WriteLine(transaction.Amount);
    }
}
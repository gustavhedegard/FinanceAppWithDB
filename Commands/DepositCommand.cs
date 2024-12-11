public class DepositCommand : Command {

    public DepositCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("deposit","Make a deposit", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        string type = args[0];
        double amount = Convert.ToDouble(args[1]);

        transactionService.ExecuteTransaction(type, amount);
        Console.WriteLine($"You successfully deposited: {amount}");
    }
}
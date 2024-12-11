public class WithdrawCommand : Command {

    public WithdrawCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("withdraw","Make a withdrawal", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        string type = args[0];
        double amount = Convert.ToDouble(args[1]);

        transactionService.ExecuteTransaction(type, -amount);
        Console.WriteLine($"You successfully withdrew: {amount}");
    }
}
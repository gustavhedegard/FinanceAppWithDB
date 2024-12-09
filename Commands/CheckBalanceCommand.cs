public class CheckBalanceCommand : Command {
    public CheckBalanceCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("check-balance","check current balance", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args)
    {
        double balance = transactionService.GetBalance();
        Console.WriteLine($"Your current balance is: {balance}");;

    }
}
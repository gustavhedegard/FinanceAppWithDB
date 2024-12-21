public class CheckBalanceCommand : Command {
    public CheckBalanceCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("check-balance", transactionService, utilitiesService) {

    }

    public override void Execute(string[] args)
    {
        double balance = transactionService.GetBalance();
        Console.WriteLine($"Your current balance is: {balance}");
        utilitiesService.PressKeyToContinue();

    }
}
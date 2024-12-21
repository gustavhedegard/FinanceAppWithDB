public class DepositCommand : Command {

    public DepositCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("deposit", transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        string type = args[0];
        double amount = Convert.ToDouble(args[1]);

        transactionService.ExecuteTransaction(type, amount);
        Console.WriteLine($"You successfully deposited: {amount}");
        utilitiesService.PressKeyToContinue();
    }
}
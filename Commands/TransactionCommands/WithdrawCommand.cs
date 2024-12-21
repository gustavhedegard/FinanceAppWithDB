public class WithdrawCommand : Command {

    public WithdrawCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("withdraw",transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        string type = args[0];
        double amount = Convert.ToDouble(args[1]);

        bool insufficientFunds = transactionService.ValidateBalance(amount);

        if(insufficientFunds == true) {
           Console.WriteLine("Insufficient funds.");
        }
        else {
        transactionService.ExecuteTransaction(type, -amount);
        Console.WriteLine($"You successfully withdrew: {amount}");
        }

        utilitiesService.PressKeyToContinue();
    }
}
using System.Runtime;

public class SearchByYearCommand : Command {
    public SearchByYearCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("search-year",transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);

        List<Transaction> transactions = transactionService.SearchByYear(year);
        double[] spentAndEarned = utilitiesService.ShowSpentAndEarned(transactions);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }

        Console.WriteLine($"Money spent : {spentAndEarned[0]}\nMoney earned : {spentAndEarned[1]}");
        utilitiesService.PressKeyToContinue();
    }
}
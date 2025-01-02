public class SearchByWeekCommand : Command {
    public SearchByWeekCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("search-week", transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);
        int week = int.Parse(args[2]);

        List<Transaction> transactions = transactionService.SearchByWeek(year, week);
        double[] spentAndEarned = utilitiesService.ShowSpentAndEarned(transactions);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }
        
        Console.WriteLine($"Money spent : {spentAndEarned[0]}\nMoney earned : {spentAndEarned[1]}");
        utilitiesService.PressKeyToContinue();
    }
}
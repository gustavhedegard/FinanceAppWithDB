public class SearchByDayCommand : Command {
    public SearchByDayCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("search-day", transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        DateTime date = DateTime.Parse(args[1]);

        List<Transaction> transactions = transactionService.SearchByDay(date);
        double[] spentAndEarned = utilitiesService.ShowSpentAndEarned(transactions);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }

        Console.WriteLine($"Money spent : {spentAndEarned[0]}\nMoney earned : {spentAndEarned[1]}");
        utilitiesService.PressKeyToContinue();
    }
}
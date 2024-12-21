public class SearchByDayCommand : Command {
    public SearchByDayCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("search-day", transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        DateTime date = DateTime.Parse(args[1]);

        List<Transaction> transactions = transactionService.SearchByDay(date);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }

        utilitiesService.PressKeyToContinue();
    }
}
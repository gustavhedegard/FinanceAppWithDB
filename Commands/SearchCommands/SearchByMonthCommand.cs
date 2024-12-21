public class SearchByMonthCommand : Command {
    public SearchByMonthCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("search-month", transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);
        int month = int.Parse(args[2]);

        List<Transaction> transactions = transactionService.SearchByMonth(year, month);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }

        utilitiesService.PressKeyToContinue();
    }
}
public class SearchByDayCommand : Command {
    public SearchByDayCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("search-day","search all transactions from a certain date (YYYY-MM-DD)", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        DateTime date = DateTime.Parse(args[1]);

        List<Transaction> transactions = transactionService.SearchByDay(date);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }
    }
}
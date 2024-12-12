public class SearchByMonthCommand : Command {
    public SearchByMonthCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("search-month","search all transactions from a month (YYYY MM)", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);
        int month = int.Parse(args[2]);

        List<Transaction> transactions = transactionService.SearchByMonth(year, month);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }
    }
}
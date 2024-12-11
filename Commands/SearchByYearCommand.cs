public class SearchByYearCommand : Command {
    public SearchByYearCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("search-year","search all transactions from a single year", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);

        List<Transaction> transactions = transactionService.SearchByYear(year);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }
    }
}
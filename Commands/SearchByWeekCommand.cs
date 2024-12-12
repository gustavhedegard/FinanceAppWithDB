public class SearchByWeekCommand : Command {
    public SearchByWeekCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("search-week","search all transactions from a certain week (YYYY WW)", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);
        int week = int.Parse(args[2]);

        List<Transaction> transactions = transactionService.SearchByWeek(year, week);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }
    }
}
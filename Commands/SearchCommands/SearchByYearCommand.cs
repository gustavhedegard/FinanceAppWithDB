using System.Runtime;

public class SearchByYearCommand : Command {
    public SearchByYearCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("search-year","search all transactions from a single year", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {
        int year = int.Parse(args[1]);

        List<Transaction> transactions = transactionService.SearchByYear(year);
        double[] spentAndEarned = ShowSpentAndEarned(transactions);

        foreach(Transaction transaction in transactions) {

            Console.WriteLine(transaction.ToString());
            Console.WriteLine("");
        }

        Console.WriteLine($"Money spent : {spentAndEarned[0]}\nMoney earned : {spentAndEarned[1]}");
    }

    public double[] ShowSpentAndEarned(List<Transaction> transactions){

        double[] spentAndEarned = new double[2];
        
        foreach(Transaction transaction in transactions) {
            if (transaction.Amount < 0) {
                spentAndEarned[0] += transaction.Amount;
            } else {
                spentAndEarned[1] += transaction.Amount;
            }
        }

        return spentAndEarned;    
    }
}
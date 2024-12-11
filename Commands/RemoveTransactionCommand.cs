public class RemoveTransactionCommand : Command {
    public RemoveTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("remove","Remove a transaction", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        List<Transaction> transactions = transactionService.GetAllTransactions();

        int i = 0;
        for (i = 0; i < transactions.Count; i++ ) {
            Console.WriteLine($"{i}\nId: {transactions[i].Id}\nAmount: {transactions[i].Amount}");
            Console.WriteLine("");
        }

        Console.WriteLine("Enter transaction to remove by id:");

        int input = int.Parse(Console.ReadLine());

        transactionService.RemoveTransaction(transactions[input].Id);
    }
}
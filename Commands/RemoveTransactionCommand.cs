public class RemoveTransactionCommand : Command {
    public RemoveTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("remove","Remove a transaction", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        List<Transaction> transactions = transactionService.GetAllTransactions();

        int i = 0;
        for (i = 0; i < transactions.Count; i++ ) {
            Console.WriteLine($"TRANSACTION\nindex: {i}\nId: {transactions[i].Id}\nAmount: {transactions[i].Amount}");
            Console.WriteLine("");
        }

        Console.WriteLine("Enter index of transaction to remove:");

        int input = int.Parse(Console.ReadLine());

        transactionService.RemoveTransaction(transactions[input].Id);
        Console.WriteLine("Removed transaction succesfully.");
    }
}
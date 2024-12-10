public class RemoveTransactionCommand : Command {
    public RemoveTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("remove","Remove a transaction", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        List<Transaction> transactions = transactionService.GetAllTransactions();

        foreach (Transaction transaction in transactions) {
            Console.WriteLine($"{transaction.Id}\n{transaction.Amount}");
            Console.WriteLine("");
        }

        Console.WriteLine("Enter transaction to remove by id:");

        Guid id = Guid.Parse(Console.ReadLine());

        transactionService.RemoveTransaction(id);
    }
}
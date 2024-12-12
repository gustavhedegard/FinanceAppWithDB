public class RemoveTransactionCommand : Command {
    public RemoveTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("remove","Remove a transaction", userService, transactionService, menuService) {

    }

    public override void Execute(string[] args) {

        List<Transaction> transactions = transactionService.GetAllTransactions();

        int i = 0;
        for (i = 0; i < transactions.Count; i++) {
            Console.WriteLine($"TRANSACTION\nindex: {i}\n{transactions[i]}");
            Console.WriteLine("");
        }

        Console.WriteLine("Enter the index of the transaction to remove:");
        string? userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int input) && input >= 0 && input < transactions.Count) {

            transactionService.RemoveTransaction(transactions[input].Id);
            Console.WriteLine("Removed transaction successfully.");
        }
        else {

            Console.WriteLine("Invalid input. Please enter a valid transaction index.");
        }
    }      
}
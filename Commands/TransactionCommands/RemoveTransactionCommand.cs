public class RemoveTransactionCommand : Command {
    public RemoveTransactionCommand(ITransactionService transactionService, IUtilitiesService utilitiesService) : base("remove",transactionService, utilitiesService) {

    }

    public override void Execute(string[] args) {

        List<Transaction> transactions = transactionService.GetAllTransactions();
        
        for (int i = 0; i < transactions.Count; i++) {
            Console.WriteLine($"TRANSACTION\nindex: {i}\n{transactions[i]}");
            Console.WriteLine("");
        }

        bool validIndex = false;
        while(!validIndex) {
            Console.WriteLine("Enter the index of the transaction to remove or press 'q' to go back to the main menu:");
            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int input) && input >= 0 && input < transactions.Count) {

                transactionService.RemoveTransaction(transactions[input].Id);
                Console.WriteLine("Removed transaction successfully.");
                validIndex = true;
            }
            else if(userInput == "q") {

                validIndex = true;
            }
            else {

                Console.WriteLine("Invalid input. Please enter a valid transaction index.");
            }
        }

        utilitiesService.PressKeyToContinue();
    }      
}
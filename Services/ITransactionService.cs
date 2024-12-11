public interface ITransactionService {
    double TransferFunds(double amount, string type);
    double GetBalance();
    void RemoveTransaction(Guid id);
    List<Transaction> GetAllTransactions();
    void ExecuteTransaction(string type, double amount);

}
public interface ITransactionService {
    double GetBalance();
    void RemoveTransaction(Guid id);
    List<Transaction> GetAllTransactions();
    void ExecuteTransaction(string type, double amount);

    List<Transaction> SearchByYear(int year);

}
public interface ITransactionService {
    double TransferFunds(double amount, string type);
    double GetBalance();
    void RemoveTransaction();
    Transaction GetTransaction();
    Transaction ExecuteTransaction(string type, double amount);

}
public interface ITransactionService {
    double TransferFunds(double amount, string type);
    double GetBalance();
    void RemoveTransaction();
    Transaction GetTransaction();
    Transaction SaveTransaction(double amount, string type);

}
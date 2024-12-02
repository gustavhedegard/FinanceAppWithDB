public interface ITransactionService {
    double TransferFunds(double amount);
    double GetBalance();
    Transaction RemoveTransaction();
    Transaction GetTransaction();
    void SaveTransaction(Guid UserId, double amount, string type);

}
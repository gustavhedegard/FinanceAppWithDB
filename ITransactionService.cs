public interface ITransactionService {
    double TransferFunds(double amount);
    double GetBalance();
    Transaction RemoveTransaction();
    Transaction GetTransaction();

}
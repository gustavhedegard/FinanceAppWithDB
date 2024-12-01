public interface ITransactionService {
    User RegisterUser(string name, string password);
    User? Login(string name, string password);
    void Logout();
    User? GetLoggedInUser();
    User CheckBalance();
    User TransferFunds();
    User RemoveTransaction();

}
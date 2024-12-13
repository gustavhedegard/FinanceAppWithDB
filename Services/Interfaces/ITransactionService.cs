public interface ITransactionService {
    double GetBalance();
    void RemoveTransaction(Guid id);
    List<Transaction> GetAllTransactions();
    void ExecuteTransaction(string type, double amount);
    List<Transaction> SearchByYear(int year);
    List<Transaction> SearchByMonth(int year, int month);
    List<Transaction> SearchByWeek(int year, int week);
    List<Transaction> SearchByDay(DateTime date);
    bool ValidateBalance(double amount);


}
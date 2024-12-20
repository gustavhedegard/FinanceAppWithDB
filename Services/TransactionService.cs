using Npgsql;
public class PostgresTransactionService : ITransactionService {

    private NpgsqlConnection _npgsqlConnection;
    private IUserService _userService;

    public PostgresTransactionService(NpgsqlConnection npgsqlConnection, IUserService userService) {
        _npgsqlConnection = npgsqlConnection;
        _userService = userService;
    }

    public double GetBalance() {
        var sql = "SELECT balance FROM users WHERE id = @id";

        var user = _userService.GetLoggedInUser();

        if(user == null) {
            throw new ArgumentException("You are not logged in."); 
        }

        using var cmd = new NpgsqlCommand(sql,_npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", user.Id);

        using var reader = cmd.ExecuteReader();
        if(!reader.Read()) {
            return -1;
        }

        double balance = reader.GetDouble(0);
        return balance;
    }

    public void RemoveTransaction(Guid id) {
        var user = _userService.GetLoggedInUser();

        if(user == null) {
            throw new ArgumentException("You are not logged in."); 
        }

        var sql = @"DELETE FROM transactions
                    WHERE id = @id;";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Transaction> GetAllTransactions() {
        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }

        string sql = @"SELECT id, user_id, amount, type, date
                       FROM transactions
                       WHERE user_id = @userId";

        var parameters = new List<KeyValuePair<string, object>>{
            new KeyValuePair<string, object>("@userId", user.Id)
        };

        var transactions = SearchTransactions(sql, parameters);

        return transactions;
        
    }
    
    public List<Transaction> SearchByYear(int year) {
        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }

        var sql = @"SELECT *
                    FROM transactions
                    WHERE EXTRACT(YEAR FROM date) = @year
                    AND user_id = @UserId;";

        var parameters = new List<KeyValuePair<string, object>>{
            new KeyValuePair<string, object>("@year", year),
            new KeyValuePair<string, object>("@userId", user.Id)
        };

        var transactions = SearchTransactions(sql, parameters);

        return transactions;
    }

    
    public List<Transaction> SearchByMonth(int year, int month) {
        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }


        var sql = @"SELECT *
                    FROM transactions
                    WHERE EXTRACT(YEAR FROM date) = @year
                    AND EXTRACT(MONTH FROM date) = @month
                    AND user_id = @UserId;";
            

        var parameters = new List<KeyValuePair<string, object>>{
            new KeyValuePair<string, object>("@year", year),
            new KeyValuePair<string, object>("@month", month),
            new KeyValuePair<string, object>("@userId", user.Id)
        };

        var transactions = SearchTransactions(sql, parameters);

        return transactions;
    }

    public List<Transaction> SearchByWeek(int year, int week) {
        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }


        var sql = @"SELECT *
                    FROM transactions
                    WHERE EXTRACT(YEAR FROM date) = @year
                    AND EXTRACT(WEEK FROM date) = @week
                    AND user_id = @UserId;";
            
        var parameters = new List<KeyValuePair<string, object>>{
            new KeyValuePair<string, object>("@year", year),
            new KeyValuePair<string, object>("@week", week),
            new KeyValuePair<string, object>("@userId", user.Id)
        };

        var transactions = SearchTransactions(sql, parameters);

        return transactions;
    }

    public List<Transaction> SearchByDay(DateTime date) {
        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }


        var sql = @"SELECT *
                    FROM transactions
                    WHERE date::DATE = @specificDate
                    AND user_id = @UserId;";
            
        var parameters = new List<KeyValuePair<string, object>>{
            new KeyValuePair<string, object>("@specificDate", date),
            new KeyValuePair<string, object>("@userId", user.Id)
        };

        var transactions = SearchTransactions(sql, parameters);

        return transactions;
    }

    public List<Transaction> SearchTransactions(string sql, List<KeyValuePair<string, object>> parameters) {

        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }

        var transactions = new List<Transaction>();
        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);

        foreach (var kvp in parameters) {
            cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
        }

        using var reader = cmd.ExecuteReader();

        while(reader.Read()) {
            var transaction = new Transaction {

                Id = reader.GetGuid(reader.GetOrdinal("id")),
                User = new User { Id = reader.GetGuid(reader.GetOrdinal("user_id")) },
                Amount = reader.GetDouble(reader.GetOrdinal("amount")),
                Type = reader.GetString(reader.GetOrdinal("type")),
                Date = reader.GetDateTime(reader.GetOrdinal("date"))
            };

            transactions.Add(transaction);
        }

        return transactions;
    }

    public void ExecuteTransaction(string type,double amount) {

        var user = _userService.GetLoggedInUser();

        if (user == null) {
            throw new ArgumentException("You are not logged in.");
        }

        var transaction = new Transaction {
            Id = Guid.NewGuid(),
            User = user,
            Amount = amount,
            Date = DateTime.Now,
            Type = type
        };

        var sql = @"
                BEGIN;
                UPDATE users
                SET balance = balance + @amount
                WHERE id = @userId;

                INSERT INTO transactions (id, user_id, amount, type, date)
                VALUES (@id, @userId, @amount, @type, @date);
                COMMIT;
            ";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", transaction.Id);
        cmd.Parameters.AddWithValue("@userId", user.Id);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@type", type);
        cmd.Parameters.AddWithValue("@date", transaction.Date);

        cmd.ExecuteNonQuery();
    }

    public bool ValidateBalance(double amount) {

        bool insufficientFunds;
        double balance = GetBalance();

        if(balance >= amount) {
           insufficientFunds = false;
        }
        else {
            insufficientFunds = true;
        }

        return insufficientFunds;
    }
}
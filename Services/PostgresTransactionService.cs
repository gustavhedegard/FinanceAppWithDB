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

        List<Transaction> transactions = new List<Transaction>();

        string sql = @"SELECT id, user_id, amount, type, date
                       FROM transactions
                       WHERE user_id = @userId";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@userId", user.Id);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var transaction = new Transaction
            {
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

    public List<Transaction> SearchByYear(int year) {
        var user = _userService.GetLoggedInUser();

        if(user == null){
            throw new ArgumentException("You're not logged in!");
        }

        List<Transaction> transactions = new List<Transaction>();

        var sql = @"SELECT *
                    FROM transactions
                    WHERE EXTRACT(YEAR FROM date) = @year;";
            
        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@year", year);

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
                         
        var sql = $@"
                DO $$
        DECLARE
            sufficient_funds DECIMAL;
        BEGIN
            -- Check the user's balance
            SELECT balance INTO sufficient_funds
            FROM users
            WHERE id = '{user.Id}';

            -- Raise an exception if insufficient funds
            IF sufficient_funds < {amount} THEN
                RAISE EXCEPTION 'Insufficient funds';
            END IF;
            END $$;

            -- Insert the transaction
            INSERT INTO transactions (id, user_id, amount, type, date)
            VALUES (@id, @userId, @amount, @type, @date);

            -- Update the user's balance
            UPDATE users
            SET balance = balance + @amount
            WHERE id = @userId;
        ";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", transaction.Id);
        cmd.Parameters.AddWithValue("@userId", user.Id);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@type", type);
        cmd.Parameters.AddWithValue("@date", transaction.Date);

        cmd.ExecuteNonQuery();
    }
}
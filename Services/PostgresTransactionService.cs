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

        using var cmd = new NpgsqlCommand(sql,_npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", user.Id);

        using var reader = cmd.ExecuteReader();
        if(!reader.Read()) {
            return -1;
        }

        double balance = reader.GetDouble(0);
        return balance;
    }

    public double TransferFunds(double amount, string type) {
        return -1;
        
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

    public Transaction GetTransaction() {
        return null;

    }

    

    public Transaction ExecuteTransaction(string type,double amount) {

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
                    SET balance = balance - @amount 
                    WHERE id = @userId; 
                COMMIT;

                    INSERT INTO transactions(id, user_id, amount, type, date) VALUES (
                    @id,
                    @userId,
                    @amount,
                    @type,
                    @date
                    );
                COMMIT;
                ";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", transaction.Id);
        cmd.Parameters.AddWithValue("@userId", user.Id);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@type", type);
        cmd.Parameters.AddWithValue("@date", transaction.Date);

        cmd.ExecuteNonQuery();

        return transaction;
    }
}
using Npgsql;
public class PostgresTransactionService : ITransactionService {

    private NpgsqlConnection npgsqlConnection;
    private Guid? _loggedInUser;

    public double GetBalance() {
        var sql = "SELECT balance FROM users WHERE id = @id";

        using var cmd = new NpgsqlCommand(sql,npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", _loggedInUser);

        var reader = cmd.ExecuteReader();
        if(!reader.Read()) {
            return -1;
        }

        double balance = reader.GetDouble(0);
        return balance;
    }

    public double TransferFunds(double amount) {
        var sql = @"INSERT INTO transactions(id, user_id, amount, date, type) VALUES (
            @id,
            @user_id,
            @amount,
            @date,
            @type
        )";

        using var cmd = new NpgsqlCommand(sql, npgsqlConnection);
        cmd.Parameters.AddWithValue()
    }
}
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
}
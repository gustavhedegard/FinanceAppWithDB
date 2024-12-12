using Npgsql;
public class PostgresUserService : IUserService {

    private NpgsqlConnection _npgsqlConnection;
    private Guid? _loggedInUser = null;

    public PostgresUserService(NpgsqlConnection npgsqlConnection) {
        _npgsqlConnection = npgsqlConnection;
    }

    public User? GetLoggedInUser() {
        if (_loggedInUser == null) {
           
            return null;
        }

        var sql = @"SELECT * FROM users WHERE id = @id";
        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", _loggedInUser);

        using var reader = cmd.ExecuteReader();
        if(!reader.Read()) {
            return null;
        }

        var user = new User {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2)
        };

        return user;
    }

    public User RegisterUser(string name, string password) {
    
        var user = new User {
            Id = Guid.NewGuid(),
            Name = name,
            Password = password,
            Balance = 0
        };

        var sql = @"INSERT INTO users (id, name, password, balance) VALUES (
            @id,
            @name,
            @password,
            @balance
        )";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@id", user.Id);
        cmd.Parameters.AddWithValue("@name",user.Name);
        cmd.Parameters.AddWithValue(@"password", user.Password);
        cmd.Parameters.AddWithValue(@"balance", user.Balance);

        cmd.ExecuteNonQuery();

        return user;

    }

    public User? Login(string name, string password){
        var sql = @"SELECT * FROM users WHERE name = @name AND password = @password";

        using var cmd = new NpgsqlCommand(sql, _npgsqlConnection);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@password", password);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) {
            return null;
        }

        var user = new User {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2)
        };

        _loggedInUser = user.Id;

        return user;
    }

    public void Logout() {
        _loggedInUser = null;
    }

    public void Close() {
        _npgsqlConnection.Close();
    }
}
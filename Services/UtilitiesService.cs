using Npgsql;
public class UtilitiesService : IUtilitiesService {
    private NpgsqlConnection _npgsqlConnection;
    private IUserService _userService;

    public UtilitiesService(NpgsqlConnection npgsqlConnection, IUserService userService) {
        _npgsqlConnection = npgsqlConnection;
        _userService = userService;
    }


    public void PressKeyToContinue() {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }

    public User ValidateUser(){
        var user = _userService.GetLoggedInUser();
        if(user == null) {
            throw new ArgumentException("You are not logged in."); 
        }

        return user;
    }
}
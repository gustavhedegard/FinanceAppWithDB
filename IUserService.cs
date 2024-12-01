public interface IUserService {
    User RegisterUser(string name, string password);
    User? Login(string name, string password);
    void Logout();
    User? GetLoggedInUser();

}
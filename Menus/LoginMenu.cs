public class LoginMenu {
    private PostgresUserService _postgresUserService;

    public LoginMenu(PostgresUserService postgresUserService) {
        _postgresUserService = postgresUserService;
    }

    public void Display() {
        Console.WriteLine("Welcome to the bank!\nPlease register a user or login");
        Console.WriteLine("1 - Login\n2 - Register new user");
        int input = int.Parse(Console.ReadLine());

        if (input == 1) {
            string name = Console.ReadLine();
            string password = Console.ReadLine();

            User? user = _postgresUserService.Login(name, password);
            if (user != null) {
                Console.WriteLine("Login complete!");
                
            }
            else {
                Console.WriteLine("Wrong name or password");
            }
        }
        else if (input == 2) {
            string name = Console.ReadLine();
            string password = Console.ReadLine();

            _postgresUserService.RegisterUser(name, password);
            
        }
    }
}
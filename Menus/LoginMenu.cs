public class LoginMenu {
    private PostgresUserService _postgresUserService;
    private BankMenu _bankMenu;

    public LoginMenu(PostgresUserService postgresUserService, BankMenu bankMenu) {
        _postgresUserService = postgresUserService;
        _bankMenu = bankMenu;
    }

    public void Display() {
        bool loggedIn = false;
        while(loggedIn == false) {

            Console.WriteLine("Welcome to the bank!\nPlease register a user or login");
            Console.WriteLine("1 - Login\n2 - Register new user");
            int input = int.Parse(Console.ReadLine());
        
            if (input == 1) {
                string name = Console.ReadLine();
                string password = Console.ReadLine();

                User? user = _postgresUserService.Login(name, password);

                if (user != null) {
                    Console.WriteLine("Login complete!");
                    loggedIn = true;
                    _bankMenu.Display(_postgresUserService);
                }
                else {
                    Console.WriteLine("Wrong name or password");
                }
            }
            else if (input == 2) {
                string name = Console.ReadLine();
                string password = Console.ReadLine();

                _postgresUserService.RegisterUser(name, password);
                Console.WriteLine($"Registred user {name} succesfully.");
                
            }
        }
    }
}
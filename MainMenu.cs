public class Menu {

    private PostgresUserService postgresUserService;
    public void MainMenu() {

        Console.WriteLine("Welcome to the Bank!");
        Console.WriteLine("Please register a user:");

        postgresUserService.RegisterUser("Gustav", "123");
    }
}
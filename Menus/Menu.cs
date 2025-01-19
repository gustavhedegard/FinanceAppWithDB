public abstract class Menu {
    protected List<Command> commands = new List<Command>();
    public void AddCommand(Command command) {
        commands.Add(command);
    }

    public void ExecuteCommand(string inputCommand) {
        string[] commandParts = inputCommand.Split(" ");

        foreach (Command command in commands) {
            if(command.Name.Equals(commandParts[0])) {
                command.Execute(commandParts);
                return;
            }
        }
       
        Console.WriteLine("Please enter a valid command");

    }
    public abstract void Display();
}
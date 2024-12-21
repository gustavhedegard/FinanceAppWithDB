public abstract class Command {
    public string Name { get; init; }
    protected IUserService? userService;
    protected ITransactionService transactionService;
    protected IMenuService? menuService;
    protected IUtilitiesService utilitiesService;

    public Command(string name, IUserService userService, ITransactionService transactionService, IMenuService menuService, IUtilitiesService utilitiesService) {
        Name = name;
        this.userService = userService;
        this.transactionService = transactionService;
        this.menuService = menuService;
        this.utilitiesService = utilitiesService;
    
    }

    protected Command(string name, ITransactionService transactionService, IUtilitiesService utilitiesService)
    {
        Name = name;
        this.transactionService = transactionService;
        this.utilitiesService = utilitiesService;
    }

    public abstract void Execute(string[] args);
}
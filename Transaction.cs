public class Transaction {
    private Guid _id { get; init;}
    public DateTime Date { get; private set; }
    public double Amount { get; private set; }
    public string Type { get; private set; }

}
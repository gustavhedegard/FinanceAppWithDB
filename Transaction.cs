public class Transaction {
    private Guid _id { get; init;}
    public Guid UserId { get; set;}
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public required string Type { get; set; }

}
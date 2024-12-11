public class Transaction {
    public Guid Id { get; init;}
    public User? User { get; set;}
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public required string Type { get; set; }

    public override string ToString() {
        return $"Transaction ID - {Id}\nUser ID - {User.Id}\nAmount - {Amount}\nCreated at - {Date}\nType - {Type}";
    }

}
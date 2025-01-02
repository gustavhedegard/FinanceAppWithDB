public interface IUtilitiesService {
    void PressKeyToContinue();
    User? ValidateUser();
    double[] ShowSpentAndEarned(List<Transaction> transactions);
}
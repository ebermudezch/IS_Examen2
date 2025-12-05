namespace ExamTwo.Repositories
{
    public interface IPaymentRepository
    {
        Dictionary<int, int> GetAvailableChange();
        void UpdateChangeInventory(int coinValue, int quantity);
    }
}

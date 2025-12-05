namespace ExamTwo.Repositories
{
    public interface ICoffeeInfoRepository
    {
        Dictionary<string, int> GetInventory();
        Dictionary<string, int> GetPrices();
        void UpdateCoffeeInventory(string coffeeName, int quantity);
    }
}

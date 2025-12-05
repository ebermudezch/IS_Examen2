using ExamTwo.Controllers;

namespace ExamTwo.Repositories
{
    public class CoffeeInfoRepository : ICoffeeInfoRepository
    {
        private readonly Database _db;

        public CoffeeInfoRepository(Database db)
        {
            _db = db;
        }

        public Dictionary<string, int> GetInventory()
        {
            return _db.coffeeInventory;
        }

        public Dictionary<string, int> GetPrices()
        {
            return _db.coffeeCost;
        }

        public void UpdateCoffeeInventory(string coffeeName, int quantity)
        {
            var coffee = _db.coffeeInventory.First(c => c.Key == coffeeName).Key;
            _db.coffeeInventory[coffee] -= quantity;
        }
    }
}

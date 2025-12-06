using ExamTwo.Repositories;

namespace ExamTwo.Services
{
    public class CoffeeService
    {
        private readonly ICoffeeInfoRepository _coffeeRepo;

        public CoffeeService(ICoffeeInfoRepository coffeeRepo)
        {
            _coffeeRepo = coffeeRepo;
        }

        public Dictionary<string, int> GetInventory()
        {
            return _coffeeRepo.GetInventory();
        }

        public Dictionary<string, int> GetPrices()
        {
            return _coffeeRepo.GetPrices();
        }

        public int CalculateTotal(Dictionary<string, int> order)
        {
            var prices = _coffeeRepo.GetPrices();
            return order.Sum(o => prices[o.Key] * o.Value);
        }

        public bool ValidateInventory(Dictionary<string, int> order)
        {
            var inventory = _coffeeRepo.GetInventory();
            foreach (var item in order)
            {
                if (!inventory.ContainsKey(item.Key) || item.Value > inventory[item.Key])
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdateInventory(Dictionary<string, int> order)
        {
            foreach (var item in order)
            {
                _coffeeRepo.UpdateCoffeeInventory(item.Key, item.Value);
            }
        }
    }
}

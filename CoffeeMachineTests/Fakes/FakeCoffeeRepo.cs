using ExamTwo.Repositories;
using System.Collections.Generic;

namespace CoffeeMachineTests.Fakes
{
    public class FakeCoffeeRepo : ICoffeeInfoRepository
    {
        private Dictionary<string, int> _inventory;
        private Dictionary<string, int> _prices;

        public FakeCoffeeRepo()
        {
            _inventory = new Dictionary<string, int>
            {
                { "Americano", 10 },
                { "Capuchino", 8 },
                { "Late", 10 },
                { "Mocachino", 15 }
            };

            _prices = new Dictionary<string, int>
            {
                { "Americano", 950 },
                { "Capuchino", 1200 },
                { "Late", 1350 },
                { "Mocachino", 1500 }
            };
        }

        public Dictionary<string, int> GetInventory() => new Dictionary<string, int>(_inventory);

        public Dictionary<string, int> GetPrices() => new Dictionary<string, int>(_prices);

        public void UpdateCoffeeInventory(string coffeeName, int quantity)
        {
            if (_inventory.ContainsKey(coffeeName))
                _inventory[coffeeName] -= quantity;
        }
    }
}

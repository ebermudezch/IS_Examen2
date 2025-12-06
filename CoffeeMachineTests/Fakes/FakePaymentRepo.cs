using ExamTwo.Repositories;
using System.Collections.Generic;

namespace CoffeeMachineTests.Fakes
{
    public class FakePaymentRepo : IPaymentRepository
    {
        private Dictionary<int, int> _coins;

        public FakePaymentRepo()
        {
            _coins = new Dictionary<int, int>
            {
                { 500, 20 },
                { 100, 30 },
                { 50, 50 },
                { 25, 25 }
            };
        }

        public Dictionary<int, int> GetAvailableChange() => new Dictionary<int, int>(_coins);

        public void UpdateChangeInventory(int coinValue, int quantity)
        {
            if (_coins.ContainsKey(coinValue))
                _coins[coinValue] -= quantity;
        }

        // Para probar casos de monedas insuficientes
        public void SetCoinQuantity(int coinValue, int quantity)
        {
            if (_coins.ContainsKey(coinValue))
                _coins[coinValue] = quantity;
        }
    }
}

using ExamTwo.Controllers;

namespace ExamTwo.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Database _db;

        public PaymentRepository(Database db)
        {
            _db = db;
        }

        public Dictionary<int, int> GetAvailableChange()
        {
            return _db.changeInventory;
        }

        public void UpdateChangeInventory(int coinValue, int quantity)
        {
            var coin = _db.changeInventory.First(c => c.Key == coinValue).Key;
            _db.changeInventory[coin] -= quantity;
        }
    }
}

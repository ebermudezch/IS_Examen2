using ExamTwo.Repositories;

namespace ExamTwo.Services
{
    public class PaymentService
    {
        private readonly IPaymentRepository _paymentRepo;

        public PaymentService(IPaymentRepository paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        public Dictionary<int, int> GetAvailableChange()
        {
            return _paymentRepo.GetAvailableChange();
        }

        public bool ValidatePayment(int totalAmount, int totalCost)
        {
            return totalAmount >= totalCost;
        }

        public bool CalculateChange(int totalAmount, int totalCost, out string changeMessage)
        {
            int change = totalAmount - totalCost;
            var available = _paymentRepo.GetAvailableChange();
            changeMessage = $"Su vuelto es de: {change} colones.\nDesglose:";

            foreach (var coin in available.Keys.OrderByDescending(c => c))
            {
                var count = Math.Min(change / coin, available[coin]);
                if (count > 0)
                {
                    changeMessage += $"\n{count} moneda(s) de {coin}";
                    change -= coin * count;
                    _paymentRepo.UpdateChangeInventory(coin, count);
                }
            }

            if (change > 0)
            {
                changeMessage = "No hay suficiente cambio en la máquina.";
                return false;
            }

            return true;
        }
    }
}

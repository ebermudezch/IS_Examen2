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

        public bool ValidatePayment(int totalAmount, int totalCost, out string errorMessage)
        {
            if (totalAmount < totalCost)
            {
                errorMessage = "Dinero insuficiente.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool CalculateChange(int totalAmount, int totalCost, out string changeMessage, out string errorMessage)
        {
            int change = totalAmount - totalCost;
            var available = _paymentRepo.GetAvailableChange();
            changeMessage = $"Su vuelto es de: {change} colones.\n Desglose:";

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
                errorMessage = "No hay suficiente cambio en la máquina.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

    }
}

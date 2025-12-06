using NUnit.Framework;
using ExamTwo.Services;
using CoffeeMachineTests.Fakes;

namespace CoffeeMachineTests
{
    public class PaymentServiceTests
    {
        private PaymentService _paymentService;
        private FakePaymentRepo _paymentRepo;

        [SetUp]
        public void Setup()
        {
            _paymentRepo = new FakePaymentRepo();
            _paymentService = new PaymentService(_paymentRepo);
        }

        [Test]
        public void GetAvailableChange_ShouldReturnInitialCoinInventory()
        {
            var change = _paymentService.GetAvailableChange();

            Assert.That(change[500], Is.EqualTo(20));
            Assert.That(change[100], Is.EqualTo(30));
            Assert.That(change[50], Is.EqualTo(50));
            Assert.That(change[25], Is.EqualTo(25));
        }

        [Test]
        public void ValidatePayment_ShouldFailWhenAmountIsLessThanCost()
        {
            var result = _paymentService.ValidatePayment(500, 950, out var error);

            Assert.IsFalse(result);
            Assert.That(error, Is.EqualTo("Dinero insuficiente."));
        }

        [Test]
        public void ValidatePayment_ShouldPassWhenAmountIsEnough()
        {
            var result = _paymentService.ValidatePayment(1000, 950, out var error);

            Assert.IsTrue(result);
            Assert.That(error, Is.EqualTo(string.Empty));
        }

        [Test]
        public void CalculateChange_ShouldReturnCorrectBreakdown()
        {
            var result = _paymentService.CalculateChange(1000, 950, out var message, out var error);

            Assert.IsTrue(result);
            Assert.That(error, Is.EqualTo(string.Empty));
            StringAssert.Contains("Su vuelto es de: 50 colones", message);
            StringAssert.Contains("1 moneda(s) de 50", message);
        }

        [Test]
        public void CalculateChange_ShouldFailWhenNotEnoughCoins()
        {
            _paymentRepo.SetCoinQuantity(50, 0);
            _paymentRepo.SetCoinQuantity(25, 0);

            var result = _paymentService.CalculateChange(1000, 950, out var message, out var error);
            Console.WriteLine(message);

            Assert.IsFalse(result);
            Assert.That(error, Is.EqualTo("No hay suficiente cambio en la máquina."));
        }
    }
}

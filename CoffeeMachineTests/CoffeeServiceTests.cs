using NUnit.Framework;
using ExamTwo.Services;
using CoffeeMachineTests.Fakes;
using System.Collections.Generic;

namespace CoffeeMachineTests
{
    public class CoffeeServiceTests
    {
        private CoffeeService _coffeeService;

        [SetUp]
        public void Setup()
        {
            var fakeCoffeeRepo = new FakeCoffeeRepo();
            _coffeeService = new CoffeeService(fakeCoffeeRepo);
        }

        [Test]
        public void GetInventory_ShouldReturnInitialCoffeeInventory()
        {
            var inventory = _coffeeService.GetInventory();

            Assert.That(inventory["Americano"], Is.EqualTo(10));
            Assert.That(inventory["Capuchino"], Is.EqualTo(8));
            Assert.That(inventory["Late"], Is.EqualTo(10));
            Assert.That(inventory["Mocachino"], Is.EqualTo(15));
        }

        [Test]
        public void GetPrices_ShouldReturnCorrectCoffeePrices()
        {
            var prices = _coffeeService.GetPrices();

            Assert.That(prices["Americano"], Is.EqualTo(950));
            Assert.That(prices["Capuchino"], Is.EqualTo(1200));
            Assert.That(prices["Late"], Is.EqualTo(1350));
            Assert.That(prices["Mocachino"], Is.EqualTo(1500));
        }

        [Test]
        public void CalculateTotal_ShouldReturnCorrectOrderTotal()
        {
            var order = new Dictionary<string, int> { { "Americano", 2 }, { "Capuchino", 1 } };

            var total = _coffeeService.CalculateTotal(order);

            Assert.That(total, Is.EqualTo(950 * 2 + 1200));
        }

        [Test]
        public void ValidateInventory_ShouldFailWhenOrderExceedsInventory()
        {
            var order = new Dictionary<string, int> { { "Capuchino", 20 } };

            var result = _coffeeService.ValidateInventory(order, out var error);

            Assert.IsFalse(result);
            Assert.That(error, Is.EqualTo("No hay suficientes Capuchino en la máquina."));
        }

        [Test]
        public void UpdateInventory_ShouldDecreaseCoffeeInventory()
        {
            var order = new Dictionary<string, int> { { "Americano", 2 } };

            _coffeeService.UpdateInventory(order);
            var inventory = _coffeeService.GetInventory();

            Assert.That(inventory["Americano"], Is.EqualTo(8));
        }
    }
}

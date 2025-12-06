using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamTwo.Services;
using ExamTwo.Models;

namespace ExamTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly CoffeeService _coffeeService;
        private readonly PaymentService _paymentService;

        public CoffeeMachineController(CoffeeService coffeeService, PaymentService paymentService)
        {
            _coffeeService = coffeeService;
            _paymentService = paymentService;
        }

        [HttpGet("getCoffees")]
        public ActionResult<Dictionary<string, int>> GetCoffees()
        {
            return Ok(_coffeeService.GetInventory());
        }

        [HttpGet("getCoffeePrices")]
        public ActionResult<Dictionary<string, int>> GetCoffeePrices()
        {
            return Ok(_coffeeService.GetPrices());
        }

        [HttpGet("getChangeQuantity")]
        public ActionResult<Dictionary<int, int>> GetChangeQuantity()
        {
            return Ok(_paymentService.GetAvailableChange());
        }

        [HttpPost("buyCoffee")]
        public ActionResult<string> BuyCoffee([FromBody] OrderRequest request)
        {
            if (request.Order == null || request.Order.Count == 0)
                return BadRequest("Orden vacía.");

            if (request.Payment.TotalAmount <= 0)
                return BadRequest("Dinero insuficiente.");

            try
            {
                var totalCost = _coffeeService.CalculateTotal(request.Order);

                if (!_coffeeService.ValidateInventory(request.Order, out var inventoryError))
                    return BadRequest(inventoryError);

                if (!_paymentService.ValidatePayment(request.Payment.TotalAmount, totalCost, out var paymentError))
                    return BadRequest(paymentError);

                _coffeeService.UpdateInventory(request.Order);

                if (!_paymentService.CalculateChange(request.Payment.TotalAmount, totalCost, out var changeMessage, out var changeError))
                    return BadRequest(changeError);

                return Ok(changeMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

        [HttpPost("calculateTotal")]
        public ActionResult<int> CalculateTotal([FromBody] Dictionary<string, int> order)
        {
            if (order == null || order.Count == 0)
                return BadRequest("Orden vacía.");

            if (!_coffeeService.ValidateInventory(order, out var error))
                return BadRequest(error);

            var total = _coffeeService.CalculateTotal(order);
            return Ok(total);
        }

    }
}

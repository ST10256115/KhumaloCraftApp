using KhumaloCraft.Models;
using KhumaloCraft.Services;
using Microsoft.AspNetCore.Mvc;
using KhumaloCraft.Services;

namespace KhumaloCraft.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderData orderData)
        {
            string instanceId = await _orderService.PlaceOrderAsync(orderData);
            return RedirectToAction("OrderStatus", new { instanceId = instanceId });
        }

        public IActionResult OrderStatus(string instanceId)
        {
          
            return View();
        }
    }
}

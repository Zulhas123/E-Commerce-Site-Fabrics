using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;
using E_Commerce_System.Session;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_System.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(Order anorder)
        {
            var products = HttpContext.Session.Get<List<Products>>("products") ?? new List<Products>();
            var productIds = products.Select(p => p.Id).ToList();

            await _orderService.CreateOrderAsync(anorder, productIds);
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }
    }
}


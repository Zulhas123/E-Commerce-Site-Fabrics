using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;
using E_Commerce_System.Session;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace E_Commerce_System.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult GetCookie()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return null!;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var products = await _productService.GetAllWithDetailsAsync();
            return View(products.ToPagedList(page ?? 1, 9));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetByIdWithDetailsAsync(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetByIdWithDetailsAsync(id.Value);
            if (product == null) return NotFound();

            var products = HttpContext.Session.Get<List<Products>>("products") ?? new List<Products>();
            products.Add(product);
            HttpContext.Session.Set("products", products);

            return View(product);
        }

        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            var products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
            var products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cart()
        {
            var products = HttpContext.Session.Get<List<Products>>("products") ?? new List<Products>();
            return View(products);
        }
    }
}


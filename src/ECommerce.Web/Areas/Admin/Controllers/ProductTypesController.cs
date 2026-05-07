using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypesController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productTypeService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (!ModelState.IsValid) return View(productTypes);

            await _productTypeService.CreateAsync(productTypes);
            TempData["Save"] = "Save Product Type Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var productType = await _productTypeService.GetByIdAsync(id.Value);
            if (productType == null) return NotFound();

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (!ModelState.IsValid) return View(productTypes);

            await _productTypeService.UpdateAsync(productTypes);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var productType = await _productTypeService.GetByIdAsync(id.Value);
            if (productType == null) return NotFound();

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productType = await _productTypeService.GetByIdAsync(id.Value);
            if (productType == null) return NotFound();

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if (id == null) return NotFound();
            if (id != productTypes.Id) return NotFound();

            await _productTypeService.DeleteByIdAsync(id.Value);
            TempData["Delete"] = "Delete Product Type Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}


using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;

namespace E_Commerce_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly ISpacialTagService _spacialTagService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            IProductService productService,
            IProductTypeService productTypeService,
            ISpacialTagService spacialTagService,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _spacialTagService = spacialTagService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithDetailsAsync();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Decimal? lowamount, Decimal? largeamount)
        {
            var products = await _productService.GetByPriceRangeWithDetailsAsync(lowamount, largeamount);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(await _spacialTagService.GetAllAsync(), "Id", "spacialTag");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (await _productService.ExistsByNameAsync(products.Name))
            {
                ViewBag.message = "This Product is already Exist!";
                ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "ProductType");
                ViewData["TagId"] = new SelectList(await _spacialTagService.GetAllAsync(), "Id", "spacialTag");
                return View(products);
            }

            if (!ModelState.IsValid) return View(products);

            if (image != null)
            {
                var name = Path.Combine(_webHostEnvironment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                products.Image = "Images/" + image.FileName;
            }

            if (image == null)
            {
                products.Image = "Images/no-image.jpg";
            }

            await _productService.CreateAsync(products);
            TempData["Save"] = "Save Product  Successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(await _spacialTagService.GetAllAsync(), "Id", "spacialTag");

            if (id == null) return NotFound();

            var product = await _productService.GetByIdWithDetailsAsync(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (await _productService.ExistsByNameAsync(products.Name, excludingId: products.Id))
            {
                ViewBag.message = "This Product is already Exist!";
                ViewData["ProductTypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "ProductType");
                ViewData["TagId"] = new SelectList(await _spacialTagService.GetAllAsync(), "Id", "spacialTag");
                return View(products);
            }

            if (!ModelState.IsValid) return View(products);

            var existing = await _productService.GetByIdAsync(products.Id);
            if (existing is null) return NotFound();

            if (image != null)
            {
                var name = Path.Combine(_webHostEnvironment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                products.Image = "Images/" + image.FileName;
            }

            if (image == null)
            {
                products.Image = existing.Image;
            }

            await _productService.UpdateAsync(products);
            TempData["Save"] = "Save Product  Successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetByIdWithDetailsAsync(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetByIdWithDetailsAsync(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null) return NotFound();

            await _productService.DeleteByIdAsync(id.Value);
            return RedirectToAction(nameof(Index));
        }
    }
}


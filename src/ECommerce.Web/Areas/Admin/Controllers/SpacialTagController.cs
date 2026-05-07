using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce_System.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpacialTagController : Controller
    {
        private readonly ISpacialTagService _spacialTagService;

        public SpacialTagController(ISpacialTagService spacialTagService)
        {
            _spacialTagService = spacialTagService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _spacialTagService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpacialTag spTag)
        {
            if (!ModelState.IsValid) return View(spTag);

            await _spacialTagService.CreateAsync(spTag);
            TempData["Save"] = "Save SpecialTag  Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tag = await _spacialTagService.GetByIdAsync(id.Value);
            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpacialTag spTag)
        {
            if (!ModelState.IsValid) return View(spTag);

            await _spacialTagService.UpdateAsync(spTag);
            TempData["Edit"] = "Edit SpecialTag  Successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tag = await _spacialTagService.GetByIdAsync(id.Value);
            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpacialTag spTag)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tag = await _spacialTagService.GetByIdAsync(id.Value);
            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, SpacialTag spTag)
        {
            if (id == null) return NotFound();
            if (id != spTag.Id) return NotFound();

            await _spacialTagService.DeleteByIdAsync(id.Value);
            TempData["Delete"] = "Delete SpecialTag  Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}


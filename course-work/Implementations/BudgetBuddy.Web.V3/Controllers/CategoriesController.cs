using BudgetBuddy.Web.V3.Models;
using BudgetBuddy.Web.V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.Web.V3.Controllers
{
    
    public class CategoriesController : Controller
    {
        private readonly ApiService _apiService;


        public CategoriesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private bool IsLoggedIn()
        {
            var token = HttpContext.Session.GetString("JWToken");
            return !string.IsNullOrEmpty(token);
        }

        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            var categories = await _apiService.GetCategoriesAsync();

            int pageSize = 5;

            // Търсене
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                categories = categories
                    .Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Сортиране
            categories = sortOrder switch
            {
                "name_desc" => categories.OrderByDescending(c => c.Name).ToList(),
                "type" => categories.OrderBy(c => c.Type).ToList(),
                "type_desc" => categories.OrderByDescending(c => c.Type).ToList(),
                _ => categories.OrderBy(c => c.Name).ToList(), // default: по име
            };

            var pagedResult = new PagedResult<CategoryViewModel>
            {
                PageNumber = page,
                TotalPages = (int)Math.Ceiling(categories.Count / (double)pageSize),
                Items = categories.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            };

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            return View(pagedResult);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _apiService.CreateCategoryAsync(model);
            if (success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error creating category.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _apiService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            var model = new UpdateCategoryViewModel
            {
                Name = category.Name,
                Type = category.Type // 👈 добави Type в CategoryViewModel ако го няма
            };

            return View(model); // подаваме правилния ViewModel
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var success = await _apiService.UpdateCategoryAsync(id, model);
            if (success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Error updating category.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _apiService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}

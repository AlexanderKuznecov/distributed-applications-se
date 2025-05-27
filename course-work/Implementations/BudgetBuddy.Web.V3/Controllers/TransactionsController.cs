using BudgetBuddy.Web.V3.Models;
using BudgetBuddy.Web.V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetBuddy.Web.V3.Controllers
{
    
    public class TransactionsController : Controller
    {
        private readonly ApiService _apiService;

        public TransactionsController(ApiService apiService)
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

            var transactions = await _apiService.GetTransactionsAsync();

            int pageSize = 5;
            sortOrder ??= "date_desc";
            // Търсене
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                transactions = transactions
                    .Where(t =>
                        t.Description?.Contains(searchString, StringComparison.OrdinalIgnoreCase) == true ||
                        t.CategoryName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Сортиране
            transactions = sortOrder switch
            {
                "date_desc" => transactions.OrderByDescending(t => t.Date).ToList(),
                "amount" => transactions.OrderBy(t => t.Amount).ToList(),
                "amount_desc" => transactions.OrderByDescending(t => t.Amount).ToList(),
                _ => transactions.OrderBy(t => t.Date).ToList(), // default: по дата
            };

            // Изчисляване на суми
            decimal totalIncome = transactions
                .Where(t => t.Type.Equals("Income", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            decimal totalExpense = transactions
                .Where(t => t.Type.Equals("Expense", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            // Пагинация
            var pagedResult = new PagedResult<TransactionViewModel>
            {
                PageNumber = page,
                TotalPages = (int)Math.Ceiling(transactions.Count / (double)pageSize),
                Items = transactions.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            };

            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchString"] = searchString;

            return View(pagedResult);
        }


        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _apiService.GetTransactionByIdAsync(id);
            return transaction == null ? NotFound() : View(transaction);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _apiService.GetCategoriesAsync();
            return View(new CreateTransactionViewModel { Categories = categories });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _apiService.GetCategoriesAsync();
                return View(model);
            }

            var success = await _apiService.CreateTransactionAsync(model);
            return success ? RedirectToAction(nameof(Index)) : BadRequest();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _apiService.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();

            var categories = await _apiService.GetCategoriesAsync();
            return View(new UpdateTransactionViewModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Description = transaction.Description,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                Categories = categories
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _apiService.GetCategoriesAsync();
                return View(model);
            }

            var success = await _apiService.UpdateTransactionAsync(id, model);
            return success ? RedirectToAction(nameof(Index)) : BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _apiService.DeleteTransactionAsync(id);
            if (!success)
            {
                // обработка при грешка
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

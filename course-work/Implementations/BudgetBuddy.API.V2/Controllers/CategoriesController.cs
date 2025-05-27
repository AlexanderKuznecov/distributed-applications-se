using BudgetBuddy.API.V2.DTOs.Category;
using BudgetBuddy.API.V2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetBuddy.API.V2.Controllers
{

    /// <summary>
    /// Контролер за управление на категории.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        /// <summary>
        /// Връща всички категории за текущия потребител.
        /// </summary>
        /// <returns>Списък с категории.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync(GetUserId());
            return Ok(categories);
        }

        /// <summary>
        /// Връща категория по ID.
        /// </summary>
        /// <param name="id">Идентификатор на категорията.</param>
        /// <returns>Данни за категорията.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(GetUserId(), id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        /// <summary>
        /// Създава нова категория.
        /// </summary>
        /// <param name="dto">Данни за създаване на категория.</param>
        /// <returns>Създадената категория.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var result = await _service.CreateAsync(GetUserId(), dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Актуализира съществуваща категория.
        /// </summary>
        /// <param name="id">Идентификатор на категорията.</param>
        /// <param name="dto">Новите данни за категорията.</param>
        /// <returns>204 NoContent ако е успешно, 404 ако категорията не е намерена.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var success = await _service.UpdateAsync(GetUserId(), id, dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Изтрива категория по ID.
        /// </summary>
        /// <param name="id">Идентификатор на категорията.</param>
        /// <returns>204 NoContent ако изтриването е успешно, 404 ако категорията не е намерена.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(GetUserId(), id);
            return success ? NoContent() : NotFound();
        }
    }
}

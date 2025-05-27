using BudgetBuddy.API.V2.DTOs.Transaction;
using BudgetBuddy.API.V2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BudgetBuddy.API.V2.Controllers
{
    /// <summary>
    /// Контролер за управление на транзакции.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Връща всички транзакции на потребителя.
        /// </summary>
        /// <returns>Списък с транзакции.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var transactions = await _transactionService.GetAllTransactionsAsync(userId);
            return Ok(transactions);
        }

        /// <summary>
        /// Връща транзакция по ID.
        /// </summary>
        /// <param name="id">Идентификатор на транзакцията.</param>
        /// <returns>Детайли за транзакцията.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetUserId();
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        /// <summary>
        /// Създава нова транзакция.
        /// </summary>
        /// <param name="dto">Обект със стойности за създаване на транзакцията.</param>
        /// <returns>Създадената транзакция.</returns>
        /*[HttpPost]
        public async Task<IActionResult> Create(TransactionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var userId = GetUserId();
            var createdTransaction = await _transactionService.CreateTransactionAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdTransaction.Id }, createdTransaction);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionCreateDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var transaction = await _transactionService.CreateTransactionAsync(dto, userId);
            return Ok(transaction);
        }


        /// <summary>
        /// Актуализира съществуваща транзакция.
        /// </summary>
        /// <param name="id">Идентификатор на транзакцията за редакция.</param>
        /// <param name="dto">Новите стойности на транзакцията.</param>
        /// <returns>Статус на операцията.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetUserId();
            var updated = await _transactionService.UpdateTransactionAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Изтрива транзакция по ID.
        /// </summary>
        /// <param name="id">Идентификатор на транзакцията за изтриване.</param>
        /// <returns>Статус на операцията.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var deleted = await _transactionService.DeleteTransactionAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Missing user ID in token");
            return int.Parse(userIdClaim);
        }
    }
}


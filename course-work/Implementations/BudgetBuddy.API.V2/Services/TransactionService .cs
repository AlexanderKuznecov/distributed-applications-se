using BudgetBuddy.API.V2.Data;
using BudgetBuddy.API.V2.DTOs.Transaction;
using BudgetBuddy.API.V2.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.API.V2.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Amount,
                    Date = t.Date,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category.Name,
                    Type = t.Type
                })
                .ToListAsync();
        }


        public async Task<TransactionDto?> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null) return null;

            return new TransactionDto
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category.Name
            };
        }

        /*public async Task<TransactionDto> CreateTransactionAsync(TransactionCreateDto dto)
        {
            var transaction = new Transaction
            {
                Description = dto.Description,
                Amount = dto.Amount,
                Date = dto.Date,
                CategoryId = dto.CategoryId,
                Type = dto.Type,
                UserId = dto.UserId
            };


            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return await GetTransactionByIdAsync(transaction.Id) ?? throw new Exception("Error creating transaction");
        }*/

        public async Task<TransactionDto> CreateTransactionAsync(TransactionCreateDto dto, int userId)
        {
            var transaction = new Transaction
            {
                Description = dto.Description,
                Amount = dto.Amount,
                Date = dto.Date,
                CategoryId = dto.CategoryId,
                Type = dto.Type,
                UserId = userId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return await GetTransactionByIdAsync(transaction.Id)
                ?? throw new Exception("Error creating transaction");
        }


        public async Task<bool> UpdateTransactionAsync(int id, TransactionUpdateDto dto)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;

            transaction.Description = dto.Description;
            transaction.Amount = dto.Amount;
            transaction.Date = dto.Date;
            transaction.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

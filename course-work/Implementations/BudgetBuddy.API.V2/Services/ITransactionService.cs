using BudgetBuddy.API.V2.DTOs.Transaction;

namespace BudgetBuddy.API.V2.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(int userId);
        Task<TransactionDto?> GetTransactionByIdAsync(int id);
        Task<TransactionDto> CreateTransactionAsync(TransactionCreateDto dto, int userId);
        Task<bool> UpdateTransactionAsync(int id, TransactionUpdateDto dto);
        Task<bool> DeleteTransactionAsync(int id);
    }
}

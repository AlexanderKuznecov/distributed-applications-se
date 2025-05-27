using BudgetBuddy.API.V2.DTOs.Category;

namespace BudgetBuddy.API.V2.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync(int userId);
        Task<CategoryDto?> GetByIdAsync(int userId, int id);
        Task<CategoryDto> CreateAsync(int userId, CreateCategoryDto dto);
        Task<bool> UpdateAsync(int userId, int id, UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int userId, int id);
    }
}

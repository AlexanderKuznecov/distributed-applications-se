using BudgetBuddy.API.V2.DTOs.User;

namespace BudgetBuddy.API.V2.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        Task<bool> UpdateUserAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto?> GetUserByIdAsync(int id);

    }

}

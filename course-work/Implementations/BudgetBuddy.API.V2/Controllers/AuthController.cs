using BudgetBuddy.API.V2.DTOs.User;
using BudgetBuddy.API.V2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.API.V2.Controllers
{
    /// <summary>
    /// Контролер за регистрация и логин на потребители.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрация на нов потребител.
        /// </summary>
        /// <param name="dto">Данни за регистрация на потребител.</param>
        /// <returns>Успешно съобщение или грешка, ако имейлът вече съществува.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            if (!result)
                return BadRequest(new { message = "User with this email already exists." });

            return Ok(new { message = "Registration successful." });
        }

        /// <summary>
        /// Логин на потребител.
        /// </summary>
        /// <param name="dto">Данни за логин на потребител.</param>
        /// <returns>JWT токен или грешка при невалидни данни.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(new { token });
        }

        /// <summary>
        /// Редактиране на потребителски данни.
        /// </summary>
        /// <param name="id">ID на потребителя.</param>
        /// <param name="dto">Нови данни за потребителя.</param>
        /// <returns>Успешно съобщение или грешка.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto dto)
        {
            var success = await _userService.UpdateUserAsync(id, dto);
            if (!success)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "User updated successfully." });
        }

        /// <summary>
        /// Изтриване на потребител.
        /// </summary>
        /// <param name="id">ID на потребителя.</param>
        /// <returns>Успешно съобщение или грешка.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "User deleted successfully." });
        }

        /// <summary>
        /// Връща информация за потребителя по ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }

    }
}

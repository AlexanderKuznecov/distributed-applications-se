using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.API.V2.DTOs.User
{
    /// <summary>
    /// DTO за логин на потребител.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Имейл адрес на потребителя.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Парола на потребителя.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}

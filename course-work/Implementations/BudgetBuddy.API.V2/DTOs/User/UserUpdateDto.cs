using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.API.V2.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UserUpdateDto
    {
        /// <summary>
        /// Потребителско име.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Имейл адрес на потребителя.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Парола (минимум 6 символа).
        /// </summary>
        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

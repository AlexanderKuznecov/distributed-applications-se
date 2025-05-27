using BudgetBuddy.API.V2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.API.V2.Enriry
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty; // "Income" или "Expense"

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        // (по желание) ако имаш User entity:
        // [ForeignKey(nameof(UserId))]
        // public User User { get; set; }
    }
}

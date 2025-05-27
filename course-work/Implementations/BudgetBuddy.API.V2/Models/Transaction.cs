namespace BudgetBuddy.API.V2.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public decimal Amount { get; set; }
        public string Type { get; set; } = null!; // "Income" or "Expense"
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}

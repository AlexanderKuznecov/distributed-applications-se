namespace BudgetBuddy.API.V2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!; // "Income" or "Expense"

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}

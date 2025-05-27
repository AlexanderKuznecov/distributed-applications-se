using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Web.V3.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }

    public class CreateTransactionViewModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; } = "Expense";

        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        public int CategoryId { get; set; }

        public List<CategoryViewModel>? Categories { get; set; }
    }

    public class UpdateTransactionViewModel : CreateTransactionViewModel
    {
        public int Id { get; set; }
    }


}

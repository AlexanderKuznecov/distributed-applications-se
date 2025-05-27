using System.ComponentModel.DataAnnotations;

namespace BudgetBuddy.Web.V3.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // 👈 добави това
    }


    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required")]
        [RegularExpression("Income|Expense", ErrorMessage = "Type must be 'Income' or 'Expense'")]
        public string Type { get; set; } = "Expense";
    }

    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required")]
        [RegularExpression("Income|Expense", ErrorMessage = "Type must be 'Income' or 'Expense'")]
        public string Type { get; set; } = "Expense";
    }
}

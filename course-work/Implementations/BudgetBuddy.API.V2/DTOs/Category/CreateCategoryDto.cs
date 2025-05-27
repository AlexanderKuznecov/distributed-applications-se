namespace BudgetBuddy.API.V2.DTOs.Category
{
    /// <summary>
    /// DTO за създаване на нова категория.
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Име на категорията.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип на категорията – "Income" или "Expense".
        /// </summary>
        public string Type { get; set; }
    }
}

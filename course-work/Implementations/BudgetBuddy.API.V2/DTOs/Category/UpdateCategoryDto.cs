namespace BudgetBuddy.API.V2.DTOs.Category
{
    /// <summary>
    /// DTO за актуализация на съществуваща категория.
    /// </summary>
    public class UpdateCategoryDto
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

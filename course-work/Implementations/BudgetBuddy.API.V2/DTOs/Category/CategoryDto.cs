namespace BudgetBuddy.API.V2.DTOs.Category
{
    /// <summary>
    /// DTO за връщане на данни за категория.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Уникален идентификатор на категорията.
        /// </summary>
        public int Id { get; set; }

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

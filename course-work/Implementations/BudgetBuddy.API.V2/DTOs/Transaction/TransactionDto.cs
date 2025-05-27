namespace BudgetBuddy.API.V2.DTOs.Transaction
{
    /// <summary>
    /// DTO за връщане на информация за транзакция.
    /// </summary>
    public class TransactionDto
    {
        /// <summary>
        /// Уникален идентификатор на транзакцията.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Кратко описание на транзакцията.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Сумата на транзакцията.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Датата на транзакцията.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор на категорията.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Име на категорията.
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        public string Type { get; set; } = null!;
    }
}

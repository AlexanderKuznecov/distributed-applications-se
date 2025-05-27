namespace BudgetBuddy.API.V2.DTOs.Transaction
{
    /// <summary>
    /// DTO за актуализация на съществуваща транзакция.
    /// </summary>
    public class TransactionUpdateDto
    {
        /// <summary>
        /// Описание на транзакцията.
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
    }
}

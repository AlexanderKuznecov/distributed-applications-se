namespace BudgetBuddy.API.V2.DTOs.Transaction
{
    /// <summary>
    /// DTO за създаване на нова транзакция.
    /// </summary>
    public class TransactionCreateDto
    {
        /// <summary>
        /// Кратко описание на транзакцията.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Сумата на транзакцията.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Датата, на която е извършена транзакцията.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип на транзакцията - "Income" или "Expense".
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор на категорията.
        /// </summary>
        public int CategoryId { get; set; }

        //public int UserId { get; set; }
    }
}

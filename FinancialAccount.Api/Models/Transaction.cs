namespace FinancialAccount.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string TransactionType { get; set; } // "Deposit" or "Withdrawal"
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public Account Account { get; set; }
    }
}

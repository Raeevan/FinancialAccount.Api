using System.Transactions;

namespace FinancialAccount.Api.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}

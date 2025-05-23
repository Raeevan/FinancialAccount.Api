using FinancialAccount.Api.Data;
using FinancialAccount.Api.Models;

namespace FinancialAccountManagement.Api.Data
{
    public static class DataSeeder
    {
        public static void Seed(FinancialDbContext context)
        {
            // --- Accounts ---
            if (!context.Accounts.Any())
            {
                var accounts = new List<Account>
                {
                    new Account { AccountNumber = "ACC-001", AccountHolder = "Alice Smith", Balance = 0 },
                    new Account { AccountNumber = "ACC-002", AccountHolder = "Bob Johnson", Balance = 0 },
                    new Account { AccountNumber = "ACC-003", AccountHolder = "Charlie Lee", Balance = 0 },
                    new Account { AccountNumber = "ACC-004", AccountHolder = "Diana Prince", Balance = 0 },
                    new Account { AccountNumber = "ACC-005", AccountHolder = "Ethan Clark", Balance = 0 },
                    new Account { AccountNumber = "ACC-006", AccountHolder = "Fiona Adams", Balance = 0 },
                    new Account { AccountNumber = "ACC-007", AccountHolder = "George Miller", Balance = 0 },
                    new Account { AccountNumber = "ACC-008", AccountHolder = "Hannah Davis", Balance = 0 },
                    new Account { AccountNumber = "ACC-009", AccountHolder = "Isaac Brown", Balance = 0 },
                    new Account { AccountNumber = "ACC-010", AccountHolder = "Jasmine Lopez", Balance = 0 },
                    new Account { AccountNumber = "ACC-011", AccountHolder = "Kevin White", Balance = 0 },
                    new Account { AccountNumber = "ACC-012", AccountHolder = "Laura Green", Balance = 0 },
                    new Account { AccountNumber = "ACC-013", AccountHolder = "Michael Scott", Balance = 0 },
                    new Account { AccountNumber = "ACC-014", AccountHolder = "Nina Kim", Balance = 0 },
                    new Account { AccountNumber = "ACC-015", AccountHolder = "Oscar Turner", Balance = 0 },
                    new Account { AccountNumber = "ACC-016", AccountHolder = "Paula Morgan", Balance = 0 },
                    new Account { AccountNumber = "ACC-017", AccountHolder = "Quincy Hall", Balance = 0 },
                    new Account { AccountNumber = "ACC-018", AccountHolder = "Rachel Young", Balance = 0 },
                    new Account { AccountNumber = "ACC-019", AccountHolder = "Samuel Parker", Balance = 0 },
                    new Account { AccountNumber = "ACC-020", AccountHolder = "Tina Brooks", Balance = 0 }
                };

                context.Accounts.AddRange(accounts);
                context.SaveChanges();
            }

            // --- Transactions ---
            if (!context.Transactions.Any())
            {
                var accounts = context.Accounts.ToList();
                var transactions = new List<Transaction>();

                var fixedDeposits = new List<decimal>
                {
                    200, 300, 150, 500, 400,
                    250, 100, 350, 450, 600,
                    150, 275, 325, 125, 200,
                    425, 300, 375, 475, 225
                };

                var fixedWithdrawals = new List<decimal>
                {
                    50, 75, 25, 100, 80,
                    60, 40, 90, 70, 120,
                    30, 45, 55, 35, 65,
                    85, 95, 20, 110, 100
                };

                for (int i = 0; i < accounts.Count; i++)
                {
                    var account = accounts[i];

                    // Add deposit
                    transactions.Add(new Transaction
                    {
                        AccountId = account.Id,
                        TransactionType = "Deposit",
                        Amount = fixedDeposits[i],
                        TransactionDate = DateTime.UtcNow.AddDays(-i)
                    });

                    account.Balance += fixedDeposits[i];

                    // Add withdrawal only if enough balance
                    if (account.Balance >= fixedWithdrawals[i])
                    {
                        transactions.Add(new Transaction
                        {
                            AccountId = account.Id,
                            TransactionType = "Withdrawal",
                            Amount = fixedWithdrawals[i],
                            TransactionDate = DateTime.UtcNow.AddDays(-i + 1)
                        });

                        account.Balance -= fixedWithdrawals[i];
                    }
                }

                context.Transactions.AddRange(transactions);
                context.SaveChanges();
            }
        }
    }
}

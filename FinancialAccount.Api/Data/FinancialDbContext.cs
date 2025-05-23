using FinancialAccount.Api.Models;
using Microsoft.EntityFrameworkCore;
using FinancialAccount.Api.Data;
using System.Collections.Generic;

namespace FinancialAccount.Api.Data
{
        public class FinancialDbContext : DbContext
        {
            public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
                : base(options) { }

            public DbSet<Account> Accounts { get; set; }
            public DbSet<Transaction> Transactions { get; set; }
        }
    
}

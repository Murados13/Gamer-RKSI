using System;
using Microsoft.EntityFrameworkCore;

namespace app.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CurrencyClass> currencies { get; set; }
        public DbSet<PlayerClass> players { get; set; }
        public DbSet<LogClass> logs { get; set; }
        public DbSet<LedgerClass> ledger { get; set; }
        public DbSet<BalanceClass> balances { get; set; }
        public DbSet<OrderClass> orders { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("db_market");
        }
    }
}
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

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("db_market");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = tcp:gamerrksi.database.windows.net,1433; Initial Catalog = GamerRKSI; Persist Security Info = False; User ID = GamerRKSI; Password=ASHAN2019-12-15; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
        }
    }
}
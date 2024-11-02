using FinancialTransactionsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactionsAPI.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}

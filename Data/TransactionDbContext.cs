using Microsoft.EntityFrameworkCore;
using BankingTransactionAPI.Models;

namespace BankingTransactionAPI.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    }
}
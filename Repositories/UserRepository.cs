using BankingTransactionAPI.Data;
using BankingTransactionAPI.Exceptions;
using BankingTransactionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingTransactionAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TransactionDbContext transactionDbContext; 

        public UserRepository(TransactionDbContext transactionDbContext){
            this.transactionDbContext = transactionDbContext;
        } 

        public async Task<User> AddUser(User user)
        {
            var result = await transactionDbContext.Users.AddAsync(user);
            await transactionDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> GetUser(string id)
        {
            var user = await transactionDbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null){
                throw new InvalidAccountIdException($"Invalid Account ID: {id}");
            }
            else{
                return user;
            }
        }

        public async Task<User> SubtractFunds(string id, float amount, Transaction transfer)
        {
            var user = await GetUser(id);
            
            if (user.Balance < amount)
            {
                throw new InsufficientFundsException("Insufficient funds.");
            }

            user.Balance -= amount;
            await transactionDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> AddFunds(string id, float amount, Transaction transfer)
        {
            var user = await GetUser(id); 
            user.Balance += amount;
            await transactionDbContext.SaveChangesAsync(); 
            return user;
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            await transactionDbContext.Transactions.AddAsync(transaction);
            await transactionDbContext.SaveChangesAsync();
            return transaction; 
        }

        public async Task<List<Transaction>> GetUserTransactions(string userId)
        {   
            await GetUser(userId); 
            return await transactionDbContext.Transactions
                .Where(t => t.DonorId == userId || t.RecipientId == userId)
                .ToListAsync();
        }
    }
}
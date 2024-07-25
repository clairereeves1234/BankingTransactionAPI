using BankingTransactionAPI.Models; 

namespace BankingTransactionAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUser(string id);
        Task<User> SubtractFunds(string id, float amount, Transaction transfer);
        Task<User> AddFunds(string id, float amount, Transaction transfer); 
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<List<Transaction>> GetUserTransactions(string userId); 
    }
}

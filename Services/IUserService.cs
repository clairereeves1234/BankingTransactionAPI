using BankingTransactionAPI.Models;

namespace BankingTransactionAPI.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserById(string id);
        Task<List<Transaction>> GetTransactionsByUserId(string userId);
        Task<List<User>> TransferFunds(Transaction transaction); 
    }
}
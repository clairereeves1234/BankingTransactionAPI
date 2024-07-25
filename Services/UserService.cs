using BankingTransactionAPI.Models;
using BankingTransactionAPI.Repositories;
using BankingTransactionAPI.Exceptions; 

namespace BankingTransactionAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(User user) => await _userRepository.AddUser(user);

        public async Task<User> GetUserById(string id) => await _userRepository.GetUser(id);

        public async Task<List<Transaction>> GetTransactionsByUserId(string userId) => await _userRepository.GetUserTransactions(userId);

        public async Task<List<User>> TransferFunds(Transaction transaction)
        {
            try
            {
                var donor = await _userRepository.SubtractFunds(transaction.DonorId, transaction.Amount, transaction);
                var recipient = await _userRepository.AddFunds(transaction.RecipientId, transaction.Amount, transaction);
                await _userRepository.AddTransaction(transaction);
                List<User> userList = new List<User>
                {
                    donor, 
                    recipient
                };
                return userList;
            }
            catch (InsufficientFundsException ex)
            {
                throw new InsufficientFundsException($"Transaction failed due to insufficient funds, {ex.Message}");
            }
            catch (InvalidAccountIdException ex)
            {
                throw new InvalidAccountIdException($"Transaction failed due to an invalid account id, {ex.Message}"); 
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while processing the transation.", ex);
            }
        }
    }
}
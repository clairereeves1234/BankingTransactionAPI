using System.ComponentModel.DataAnnotations;

namespace BankingTransactionAPI.Models
{
    public class Transaction
    {
        public required string DonorId { get; set;}
        public required string RecipientId { get; set;}

        [Range(0, float.MaxValue, ErrorMessage = "Transaction amount must not be negative.")]
        public required float Amount { get; set;}
        public DateTime DateTime{ get; set;} = DateTime.UtcNow;
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();
    }
}
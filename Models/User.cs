using System.ComponentModel.DataAnnotations;

namespace BankingTransactionAPI.Models
{
    public class User
    {
        public string UserId { get; set;} = Guid.NewGuid().ToString(); 
        public required string FirstName { get; set;}
        public required string LastName { get; set;}
        
        [Range(0, float.MaxValue, ErrorMessage = "Initial balance must not be negative.")]
        public float Balance {get; set;}
    }
}
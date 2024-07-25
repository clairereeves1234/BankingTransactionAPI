namespace BankingTransactionAPI.Exceptions
{
    public class InvalidAccountIdException : Exception
    {
        public InvalidAccountIdException(string message) : base(message)
        {
        }
    }
}
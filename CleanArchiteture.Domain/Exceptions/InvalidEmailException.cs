using CleanArchiteture.Domain.Exceptions;

namespace CleanArchiteture.Domain.Middlewares
{
    public class InvalidEmailException : AppException
    {
        public InvalidEmailException(string message) : base(message, "404")
        {
        }
    }
}

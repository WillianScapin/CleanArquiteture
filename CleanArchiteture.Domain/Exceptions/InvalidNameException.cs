using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Exceptions
{
    public sealed class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(GenerateMessage(message))
        {
        }

        private static string GenerateMessage(string name)
        {
            string message = "Email inválido: " + name;
            return message;
        }
    }
}

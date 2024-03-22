using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Exceptions
{
    public class NameException : AppException
    {
        public NameException(string message) : base(message, "404")
        {
            
        }
    }
}

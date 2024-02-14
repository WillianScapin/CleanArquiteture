using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Exceptions
{
    public class AppException : Exception
    {
        public string ErrorCode { get; }
        public object Details { get; }

        public AppException(string message, string errorCode, object details = null)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}

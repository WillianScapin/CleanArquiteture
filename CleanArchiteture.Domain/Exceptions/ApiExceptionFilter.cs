using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Exceptions
{
    public sealed class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is AppException appException)
            {
                var response = new
                {
                    StatusCode = appException.ErrorCode,
                    Message = appException.Message
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)Enum.Parse(typeof(HttpStatusCode), appException.ErrorCode)
                };
            }
        }
    }
}

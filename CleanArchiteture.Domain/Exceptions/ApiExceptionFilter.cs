using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

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

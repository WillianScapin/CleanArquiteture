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
                List<string> errorList = [.. appException.Message.Split(Environment.NewLine)];

                var response = new
                {
                    StatusCode = appException.ErrorCode,
                    Message = errorList
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)Enum.Parse(typeof(HttpStatusCode), appException.ErrorCode)
                };
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Middlewares
{
    public class CustonException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustonException> _logger;

        public CustonException(RequestDelegate next, ILogger<CustonException> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong: {e}");
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the Exception Middleware will not be executed");
                    throw;
                }
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; ;
            context.Response.ContentType = "application/json";

            var error = new
            {
                Message = e.Message,
                StatusCode = (int?)HttpStatusCode.InternalServerError,
                Date = DateTime.Now
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
        }
    }
}

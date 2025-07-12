namespace CleanArquiteture.WebAPI.Middleware
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                else
                    // Se o cabeçalho já existe, você pode optar por sobrescrever ou ignorar
                    context.Request.Headers["Authorization"] = "Bearer " + token;
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline
    public static class JwtCookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtCookieMiddleware>();
        }
    }
}

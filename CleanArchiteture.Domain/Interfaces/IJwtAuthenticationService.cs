using Microsoft.AspNetCore.Http;

namespace CleanArquiteture.WebAPI.AuthenticationServices
{
    public interface IJwtAuthenticationService
    {
        string GenerateToken(Guid UserId);
        void SetTokenCookie(HttpContext context, string token);
        Guid GetTokenInfo(HttpRequest Request);
    }
}
using Microsoft.AspNetCore.Http;

namespace CleanArquiteture.WebAPI.AuthenticationServices
{
    public interface IJwtAuthenticationService
    {
        string GenerateToken(Guid UserId);
        Guid GetTokenInfo(HttpRequest Request);
    }
}
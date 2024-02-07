using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace CleanArquiteture.WebAPI.AuthenticationServices
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        //Modelo para gerar um token jwt
        public string GenerateToken(Guid UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //Crio minha chave para criptografar meu token
            var key = Encoding.ASCII.GetBytes("rbs38-8343fhye-64193-ndr27utrangplecy");
            //crio um objeto para passar minhas configurações para o token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Crio minhas Claims que contem os valores do token
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(type: "UserId", value : Convert.ToString(UserId))
                    //new Claim(ClaimTypes.Role, isPremiumAccount? Role.Premium : Role.Default),
                }),

                //Defino minha data de expiração
                //Expires = DateTime.UtcNow.AddMinutes(10080),

                //Defino meu método de criptografia
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Crio meutoken
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }


        public Guid GetTokenInfo(HttpRequest Request)
        {
            //Leio meu token
            var header = Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization"));

            //Pego meu token
            var token = header.Value.First().Replace("Bearer ", "");

            //Crio um objeto para ler meu jwt
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            var claims = jwtSecurityToken.Claims.ToList();

            Guid id = Guid.Parse(claims[0].Value);

            return id;
        }

    }
}

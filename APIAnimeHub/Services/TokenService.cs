using APIAnimeHub.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIAnimeHub.Services
{
    public class TokenService
    {
        // Permite acessar configurações do projeto
        // (appsettings.json, User Secrets, variáveis de ambiente, etc.)
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            // Recupera a chave secreta armazenada nos User Secrets.
            // Essa chave é utilizada para assinar o token e garantir
            // que ele não seja alterado por terceiros.
            var secret = _configuration["Jwt:Secret"];

            Console.WriteLine(secret);

            // Responsável por criar e escrever tokens JWT.
            var handler = new JwtSecurityTokenHandler();

            // O algoritmo de assinatura trabalha com bytes,
            // então convertemos a string da chave para UTF-8.
            var key = Encoding.UTF8.GetBytes(secret);

            // Define:
            // 1. Qual chave será utilizada para assinar o token.
            // 2. Qual algoritmo será utilizado (HMAC SHA256).
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            );

            // Define as informações do token.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Informações do usuário (Claims)
                Subject = GenerateClaims(user),

                // Tempo de expiração do token
                Expires = DateTime.UtcNow.AddHours(3),

                // Credenciais utilizadas para assinar o token
                SigningCredentials = credentials,

                Issuer = _configuration["Jwt:Issuer"],

                Audience = _configuration["Jwt:Audience"],
            };

            // Cria o objeto do token baseado nas configurações acima.
            var token = handler.CreateToken(tokenDescriptor);

            // Converte o token para string para poder enviá-lo ao frontend.
            var strToken = handler.WriteToken(token);

            return strToken;
        }

        public static ClaimsIdentity GenerateClaims(User user)
        {
            // Claims são informações sobre o usuário
            // que ficarão armazenadas dentro do token.
            var ci = new ClaimsIdentity();

            // Nome do usuário autenticado.
            // Neste caso estamos usando o Email como identificador.
            ci.AddClaim(
                new Claim(
                    ClaimTypes.Name,
                    user.Email
                )
            );

            ci.AddClaim(
                new Claim(
                        ClaimTypes.NameIdentifier, user.Id.ToString()
                    )
            );

            return ci;
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Models.Security;

namespace WebApiProductCRUD.Services
{
    public class TokenService : ITokenService
    {
        private const int _expireTimeInMinutes = 60;
        public Token GenerateToken(DbUser dbUser)
        {
            var createdToken = DateTime.Now;
            var cretedTokenUtc = DateTime.UtcNow;
            var expireToken = createdToken.AddMinutes(_expireTimeInMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Const.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, dbUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, dbUser.Email.ToString()),
                }),
                Expires = cretedTokenUtc.AddMinutes(_expireTimeInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return new Token
            {
                Created = createdToken.ToString("yyyy-MM-dd HH:mm:ss:ff"),
                Expiration = expireToken.ToString("yyyy-MM-dd HH:mm:ss:ff"),
                AccessToken = accessToken,
            };
        }
    }
}

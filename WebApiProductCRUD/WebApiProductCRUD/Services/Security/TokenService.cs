using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Models.Security;

namespace WebApiProductCRUD.Services.Security
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


            (var claims, var identity) = GetClaimsAndIdentity(dbUser);

            var tokenDescriptor = GenerateSecurityTokenDescriptor(key, claims, identity, cretedTokenUtc);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            return new Token
            {
                Created = createdToken.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expireToken.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = accessToken,
            };
        }

        private SecurityTokenDescriptor GenerateSecurityTokenDescriptor(byte[] key, List<Claim> claims,
            ClaimsIdentity identity, DateTime cretedTokenUtc)
        {
            return new SecurityTokenDescriptor
            {
                Issuer = JwtConst.Issuer,
                Audience = JwtConst.Audience,
                Subject = identity,
                Expires = cretedTokenUtc.AddMinutes(_expireTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = ConvertClaimsToDict(claims),
            };
        }

        private (List<Claim> claims, ClaimsIdentity identity) GetClaimsAndIdentity(DbUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Login"),
                claims
            );

            return (claims, identity);
        }

        private IDictionary<string, object> ConvertClaimsToDict(List<Claim> claims)
        {
            var result = new Dictionary<string, object>();
            claims.ForEach(c => result.TryAdd(c.Type, c.Value));
            return result;
        }
    }
}

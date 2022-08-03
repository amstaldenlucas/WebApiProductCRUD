using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Services;
using WebApiProductCRUD.Utils;

namespace WebApiProductCRUD.Models.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (!BoolUtil.NullTo(context.User.Identity?.IsAuthenticated))
            {
                string token = string.Empty;

                // convert cookie to jwt
                var cvalue = context.Request.Cookies[JwtConst.CookieName];
                if (string.IsNullOrWhiteSpace(cvalue))
                {
                    //await CheckForRefreshTokenCookie(context);
                }
                else
                    context.Request.Headers.Append("Authorization", "Bearer " + cvalue);

                if (context.Request.Headers.ContainsKey("Authorization"))
                    token = GetTokenFromHeadersAuthorization(context);

                // This validates the token and applies claims the to the context
                if (!string.IsNullOrWhiteSpace(token))
                {
                    try { context.User = ValidateToken(token); }
                    catch (SecurityTokenSignatureKeyNotFoundException) { /* Token expired... do nothing */ }
                    catch (SecurityTokenExpiredException)
                    {
                        //if (await CheckForRefreshTokenCookie(context))
                        if (1 == 1)
                        {
                            token = GetTokenFromHeadersAuthorization(context);
                            context.User = ValidateToken(token);
                        }
                    }
                    catch{ }
                }

            }

            await _next(context);

            // Appropriate response for unathorized request in Web area
            var reqPath = context.Request.Path.Value;
            reqPath ??= string.Empty;
            if (IsWebAreas(reqPath) && context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect("/Identity/Account/Login");
            }

        }

        private bool IsWebAreas(string reqPath)
        {
            return reqPath.Contains($"/Web/")
                || reqPath.Contains($"/Identity/");
        }

        private string GetTokenFromHeadersAuthorization(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];
            return token.Replace("Bearer ", string.Empty).Replace("Bearer", string.Empty);
        }

        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            var key = Encoding.ASCII.GetBytes(Const.Secret);

            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                ValidAudience = JwtConst.Audience,
                ValidIssuer = JwtConst.Issuer
            };
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);

            ClaimsPrincipal principal = new JwtSecurityTokenHandler()
                .ValidateToken(jwtToken, validationParameters, out _);
            return principal;
        }
    }
}

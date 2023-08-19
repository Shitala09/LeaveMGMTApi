using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using LeaveMGMTApi.Helpers;

namespace LeaveMGMTApi.Middlewares
{
    public class TokenValidatorMiddleware
    {
        private readonly RequestDelegate _nextReq;

        public TokenValidatorMiddleware(RequestDelegate nextReq)
        {
            _nextReq = nextReq;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (ValidateToken(httpContext))
            {
                await _nextReq(httpContext);
                return;
            }
            else
            {
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { message = "Unauthorized", StatusCode = StatusCodes.Status401Unauthorized }));
            }
        }

        private bool ValidateToken(HttpContext httpContext)
        {
            try
            {
                var isTokenValidation = ConfigHelper.GetConfigStr("TokenValidation");

                if (isTokenValidation == "0")
                {
                    return true;
                }

                var allowAnonimusList = ConfigHelper.GetArraySection("AllowAnonimousAccess");
                if (allowAnonimusList.Contains(httpContext.Request.Path.ToString().ToLower()))
                {
                    return true;
                }

                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var tokeHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(ConfigHelper.GetConfigStr("Jwt:Key"));
                IdentityModelEventSource.ShowPII = true;
                var tokenclaims = tokeHandler.ValidateToken(token,
                       new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(key),
                           ValidateIssuer = false,
                           ValidateAudience = false,
                           // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                           ClockSkew = TimeSpan.Zero
                       }, out SecurityToken validatedToken);

                var tokenval = validatedToken;

                string user = tokenclaims?.FindFirst("id")?.Value;
                if (!string.IsNullOrEmpty(user))
                {
                    Microsoft.Extensions.Primitives.StringValues sval = new Microsoft.Extensions.Primitives.StringValues(user);
                    httpContext.Request.Headers.Add("id", sval);
                }
                return true;
            }
            catch { return false; }
        }
    }
}



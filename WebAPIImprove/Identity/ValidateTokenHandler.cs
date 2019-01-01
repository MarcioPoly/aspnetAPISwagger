using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAPIImprove.Identity
{

    public class ValidateTokenHandler : DelegatingHandler
    {
        private const string sec = "2bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd32bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd32bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3";
        private string token;
        private HttpStatusCode statusCode;
        private static bool RetrieveToken(HttpRequestMessage request, out string token)
        {
            IEnumerable<string> headers;
            token = null;
            if (!request.Headers.TryGetValues("Authorization", out headers) || headers.Count() > 1)
            {
                return false;
            }
            var bearerToken = headers.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!RetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }
            try
            {
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
                SecurityToken securityToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = "http://localhost:52940",
                    ValidIssuer = "http://localhost:52940",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.tokenLifetimeValidator,
                    IssuerSigningKey = securityKey
                };
                Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out securityToken);
                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException e)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });
        }

        public bool tokenLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }




}
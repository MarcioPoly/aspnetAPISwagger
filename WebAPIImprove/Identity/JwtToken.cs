using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebAPIImprove.Identity
{
    public static class JwtToken
    {
        private const string tokenstring = "2bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd32bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd32bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3";
        public static string createToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity objclaimsIdentity = new ClaimsIdentity(new[]{new Claim(ClaimTypes.Name, username)});
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(tokenstring));
            var credentials = new SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);
            var objtoken =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(
                        issuer: "http://localhost:52940",
                        audience: "http://localhost:52940",
                        subject: objclaimsIdentity,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddDays(7),
                        signingCredentials: credentials

                        );
            var tokenString = tokenHandler.WriteToken(objtoken);
            return tokenString;
        }
    }
}
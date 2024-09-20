using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SlnMain.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SlnMain.Domain.Models;

namespace SlnMain.Aplication.Services
{
    public class AuthService
    {
        public AuthService()
        {
        }
        public string validate(UsuarioDto request, string secretKey)
        {

            if (request.mail == "AP@GMAIL.COM" && request.password == "123")
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.mail));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokenCreato = tokenHandler.WriteToken(tokenConfig);
                
            }

            return "ok";
        }
           
        

    }
}
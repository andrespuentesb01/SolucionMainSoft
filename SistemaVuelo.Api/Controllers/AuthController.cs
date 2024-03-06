using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using System.Text;
using SlnMain.Domain.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SlnMain.Aplication.Repository;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/Auth")]
        public class AuthController : ControllerBase
        {



        private readonly string secretKey;


        public AuthController(IConfiguration config)
        {
           
            secretKey = config.GetSection("settings").GetSection("secreteky").ToString();
        }
  
        static readonly HttpClient client = new HttpClient();

        [HttpPost("validate")]
        public IActionResult validate([FromBody] Usuario request)
        {
            //Create a task automatically assigning it to status PENDIENTE
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
                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreato });
            }

            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
        }




    }

}
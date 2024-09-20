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

using SlnMain.Infrastructure;
using Microsoft.Extensions.Configuration;
using SlnMain.Aplication.Services;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/Auth")]
        public class AuthController : ControllerBase
        {

        private readonly string secretKey;
        private readonly IUserService _userService;
        private readonly IConfiguration configuration;
        static readonly HttpClient client = new HttpClient();

        UserService userRepository;

        public AuthController(IConfiguration config, IConfiguration _configuration, IUserService userService)
        {
      
            secretKey = config.GetSection("settings").GetSection("secreteky").ToString();
            _userService = userService;
            configuration = _configuration;

        }      

        [HttpPost("validate")]
        public IActionResult validate([FromBody] UsuarioDto request)
        {
            //Create a task automatically assigning it to status PENDIENTE
          
            string tokenCreated = _userService.validateUser(request);
           
            if (tokenCreated != null)
            {
               
                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreated });
            }

            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
        }

        }

}
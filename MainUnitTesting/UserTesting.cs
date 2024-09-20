using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SlnMain.Api.Controllers;
using SlnMain.Aplication.Services;
using SlnMain.Infrastructure;

namespace MainUnitTesting
{
    public class UserTesting 
    {

        private readonly UserController _controller;
        private readonly DbCarvajalContext _dbcontext;
        private readonly IConfiguration _configuration;
        private readonly IUserService _service;

        public UserTesting(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbcontext = new DbCarvajalContext();
            _service = new UserService(_dbcontext, _configuration);
            


        }



    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using System.Text;
using SlnMain.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;
using SlnMain.Infrastructure;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/user")]
        public class UserController : ControllerBase
        {


        private readonly DbRentCarContext _dbcontext;


        public UserController(DbRentCarContext dbcontext)
        {
           
            _dbcontext = dbcontext;
        }
  
        static readonly HttpClient client = new HttpClient();



        [HttpPost("createUser")]
        [Authorize]
        public async Task<ActionResult<List<User>>> createUser([FromQuery] string name, string lastname, string cc, string drivePermision)
        {
            // insert user entity framework
            using (_dbcontext)
            {
                var user = _dbcontext.Set<User>();
                user.Add(new User { Name = name, Lastname = lastname, Cc = cc, DrivePermision = drivePermision });
                _dbcontext.SaveChanges();
            }

            return Ok(new List<User>());
        }



        [HttpGet("getUsers")]
        [Authorize]
        public async Task<ActionResult<List<User>>> getUsers()
        {
            //Execute entity framework function
            var listOfUsers = _dbcontext.Users.ToList();
            return listOfUsers;


        }




    }

}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Azure;
using SlnMain.Domain;
using System.Text;
using SlnMain.Aplication.Repository;
using Microsoft.AspNetCore.Authorization;
using SlnMain.Infrastructure;
using SlnMain.Domain.Models;
using System.Security.Cryptography;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;


namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/rent")]
        public class RentController : ControllerBase
        {

        RentRepository rentRepository;
        private readonly DbRentCarContext _dbcontext;
        private readonly IConfiguration configuration;

        public RentController(DbRentCarContext dbcontext, IConfiguration _configuration)
        {
            
            _dbcontext = dbcontext;
            configuration = _configuration;
        }
  
        static readonly HttpClient client = new HttpClient();

        [HttpPost("createRentDisponibility")]
        [Authorize]
        public async Task<ActionResult<List<Rent>>> createRentDisponibility([FromQuery] int idCar, int idUser, string comments)
        {
            //Execute the repository function to list.
            rentRepository = new RentRepository(_dbcontext, configuration);
            var listRent = rentRepository.CreateRentDisponibility( idCar, idUser, comments);
            return Ok(listRent);
        }



        [HttpGet("getRent")]
        [Authorize]
        public async Task<ActionResult<List<Rent>>> getRent()
        {
            //Execute the entity framework function to list.
            var listOfRent = _dbcontext.Rents.ToList();
            return listOfRent;

        }

        [HttpGet("getRentDetails")]
        [Authorize]
        public async Task<ActionResult<List<RentDetails>>> getRentDetails()
        {
            //Execute the repository function to list.
            rentRepository = new RentRepository(_dbcontext, configuration);
            var listRent = rentRepository.GetRentDetails();
            return Ok(listRent);


        }



     }

}
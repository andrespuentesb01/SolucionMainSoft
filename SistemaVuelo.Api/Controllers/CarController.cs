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
using System.Xml.Linq;

namespace SlnMain.Api.Controllers
{

 
        [ApiController]
        [Route("api/car")]
        public class CarController : ControllerBase
        {

        CarRepository CarRepository;
        private readonly DbRentCarContext _dbcontext;


        public CarController(DbRentCarContext dbcontext)
        {
           
            _dbcontext = dbcontext;
        }
  
        static readonly HttpClient client = new HttpClient();



        [HttpPost("createCar")]
        [Authorize]
        public async Task<ActionResult<List<Car>>> createCar([FromQuery] string plate, string branch, string year, string model)
        {
            //Create a car in database
            CarRepository = new CarRepository(_dbcontext);
            CarRepository.createCar(plate, branch, year, model);
            return Ok(new List<Car>());
        }



        [HttpGet("getCars")]
        [Authorize]
        public async Task<ActionResult<List<Car>>> getCars()
        {
            //Execute function in repository
            var listOfCars = _dbcontext.Cars.ToList();
            return listOfCars;

        }


        [HttpGet("getCarsFilter")]
        [Authorize]
        public async Task<ActionResult<List<Car>>> getCarsFilter(int idDelivery,int idCollect )
        {
            //Execute the repository function to list.
            CarRepository = new CarRepository(_dbcontext);
            var ListCars = CarRepository.getCarsFilter(idDelivery, idCollect);
            return Ok(ListCars);

        }

        [HttpGet("getCarsNewSite")]
        [Authorize]
        public async Task<ActionResult<List<Car>>> getCarsNewSite(int idDelivery, int idCollect)
        {
            //Execute the repository function to list.
            CarRepository = new CarRepository(_dbcontext);
            var ListCars = CarRepository.getCarsNewSite();
            return Ok(ListCars);

        }




    }

}
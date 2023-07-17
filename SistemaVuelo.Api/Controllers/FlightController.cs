using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVuelos.DAL.DbContext;
using SistemaVuelos.DAL.DTO;
using SistemaVuelos.DAL.DTONewShore;
using SistemaVuelos.BLL.Services;
using System;
using System.Collections.Generic;
using Azure;
using SistemaVuelos.Entity;

namespace SistemaVuelo.Api.Controllers
{
    [ApiController]
    [Route("api/flights")]
    public class FlightController : ControllerBase
    {
    

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly DbNewShoreContext _dbcontext;


        public FlightController(ILogger<WeatherForecastController> logger, DbNewShoreContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
  
        static readonly HttpClient client = new HttpClient();

        [HttpGet("GetByData")] 
        public async Task<ActionResult<List<FlightNewShoreDto>>> GetByData([FromQuery] FindFlyDto data)
        {
            
            var response = await client.GetAsync($"https://recruiting-api.newshore.es/api/flights/0");

            var content = await response.Content.ReadAsStringAsync();
    
            List<FlightNewShoreDto> flights = JsonConvert.DeserializeObject<List<FlightNewShoreDto>>(content);
            List<FlightNewShoreDto> flightsFIlter = new List<FlightNewShoreDto>();
            List<FlightNewShoreDto> flightsFIlter2 = new List<FlightNewShoreDto>();
            List<FlightNewShoreDto> flightsFIlter2Escalas = new List<FlightNewShoreDto>();
            FlightNewShoreDto flightFilter = new FlightNewShoreDto();
            string caminoTemp = "";
            string camino = "";
            int precio = 0;

            foreach (var i in flights)
            {

                if (i.departureStation == data.Origin)//inicio
                {
              
                    if (i.arrivalStation == data.Destination)
                    {
                        flightFilter.departureStation = data.Origin;
                        flightFilter.arrivalStation = data.Destination;
                        flightFilter.price = i.price;

                        flightsFIlter.Add(flightFilter);
                        camino = caminoTemp + data.Origin + "-" + data.Destination + " \\ ";
                    }
                    else 
                    {
                        flightFilter = new FlightNewShoreDto();
                        flightFilter.departureStation = i.departureStation;
                        flightFilter.arrivalStation = i.arrivalStation;
                        flightFilter.price = i.price;
                        flightsFIlter2.Add(flightFilter);
                        caminoTemp = caminoTemp + data.Origin + "-" + data.Destination + " \\ ";
                        

                    }

                }
            }

            foreach (var i in flights)//iteracion para vuelos con escalas
            {
                foreach (var ii in flightsFIlter2)//iteracion para vuelos con escalas
                {

                    if (i.departureStation == ii.arrivalStation)//inicio
                    {

                        if (i.arrivalStation == data.Destination)


                        {

                            flightFilter = new FlightNewShoreDto();
                            flightFilter.departureStation = ii.departureStation;
                            flightFilter.arrivalStation = ii.arrivalStation;
                            flightFilter.price = ii.price;
                            flightsFIlter.Add(flightFilter);
                            flightFilter = new FlightNewShoreDto();
                            flightFilter.departureStation = i.departureStation ;
                            flightFilter.arrivalStation = data.Destination;
                            flightFilter.price = i.price;

                            flightsFIlter.Add(flightFilter);
                            camino = caminoTemp + data.Origin + "-" + data.Destination + " \\ ";
                        }
                        else
                        {
                            flightFilter = new FlightNewShoreDto();
                            flightFilter.departureStation = i.departureStation;
                            flightFilter.arrivalStation = i.arrivalStation;
                            flightsFIlter2Escalas.Add(flightFilter);
                            caminoTemp = caminoTemp + data.Origin + "-" + data.Destination + " \\ ";

                        }

                    }
                } 
            
            }


            //Console.WriteLine(account.Email);

          
                return flightsFIlter;
            
        }

        [HttpGet("GetAllFlightsEntityFrame")] 
        public async Task<ActionResult<List<FlightDto>>> GetAllEntityFrame()
        {
            var List = await _dbcontext.TblFlights.Select(
                s => new FlightDto
                {
                    Id = s.Id,
                    Origin = s.Origin,
                    Destination = s.Destination,
                    Price = s.Price,
                    IdTransport = s.IdTransport
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }


        [HttpGet("GetAllWithAPi")]
        public async Task<ActionResult<List<FlightNewShoreDto>>> GetAllWithApi()
        {
            var response = await client.GetAsync($"https://recruiting-api.newshore.es/api/flights/0");

            var content = await response.Content.ReadAsStringAsync();

            List<FlightNewShoreDto> flights = JsonConvert.DeserializeObject<List<FlightNewShoreDto>>(content);

            return flights;
        }



        [HttpGet("GetUsersWithSP")]
        public async Task<ActionResult<List<TblFlight>>> GeltWithSp([FromQuery] FindFlyDto data)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Origin",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 3,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = data.Origin
                        },
                        new SqlParameter() {
                            ParameterName = "@Destination",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 3,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = data.Destination
                        }};

            var List = _dbcontext.TblFlights.FromSqlRaw("[dbo].[sp_GetFlights] @Origin, @Destination", param).ToList();





            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }

        }
    }
    
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVuelos.DAL.DbContext;
using SistemaVuelos.DAL.DTO;
using System.Collections.Generic;

namespace SistemaVuelo.Api.Controllers
{
    [ApiController]
    [Route("api/Transport")]
    public class TransportController : ControllerBase
    {
    

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly DbNewShoreContext _dbcontext;

    
        public TransportController(ILogger<WeatherForecastController> logger, DbNewShoreContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
        private static readonly string[] Summaries = new[]
      {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<TransportDto>>> Get()
        {
            var List = await _dbcontext.TblTransports.Select(
                s => new TransportDto
                {
                    Id = s.Id,
                    FlightCarrier = s.FlightCarrier,
                    FlightNumber = s.FlightNumber
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
    }
    
}
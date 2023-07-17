using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVuelos.DAL.DbContext;
using SistemaVuelos.DAL.DTO;
using System.Collections.Generic;

namespace SistemaVuelo.Api.Controllers
{
    [ApiController]
    [Route("api/Journey")]
    public class JourneyController : ControllerBase
    {
    

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly DbNewShoreContext _dbcontext;

    
        public JourneyController(ILogger<WeatherForecastController> logger, DbNewShoreContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
        private static readonly string[] Summaries = new[]
      {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        [HttpGet("GetJourneys")]
        public async Task<ActionResult<List<JourneyDto>>> Get()
        {
            var List = await _dbcontext.TblJourneys.Select(
                s => new JourneyDto
                {
                    Id = s.Id,
                    IdFlight = s.IdFlight,
                    Date = s.Date
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
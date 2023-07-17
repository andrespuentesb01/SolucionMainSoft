using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVuelos.DAL.DbContext;

namespace SistemaVuelos.RestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Cartera")]
    public class UserController : ControllerBase
    {
        private readonly DbNewShoreContext DBContext;

        public UserController(DbNewShoreContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("byUserId")]
        //[Produces(typeof(IEnumerable<VehicleStateDto>))]
        public string Get()
        {
            return ("a");
        }

    }
}

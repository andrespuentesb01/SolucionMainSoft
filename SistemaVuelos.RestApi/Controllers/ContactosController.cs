using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVuelos.DAL.DbContext;

namespace SistemaVuelos.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly DbNewShoreContext _context;

        public ContactosController(DbNewShoreContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public string GetContactoItems()
        {

            return "a";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVuelos.DAL.DTO
{
    public class JourneyDto
    {

        public int Id { get; set; }

        public int IdFlight { get; set; }

        public DateTime Date { get; set; }
    }
}

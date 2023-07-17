using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVuelos.DAL.DTO
{
    public class FlightDto
    {

        public int Id { get; set; }

        public int IdTransport { get; set; }

        public string Origin { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public double Price { get; set; }
    }
}

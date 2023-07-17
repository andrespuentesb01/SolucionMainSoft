using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVuelos.DAL.DTONewShore
{
    public class FlightNewShoreDto
    {

  
        public string departureStation { get; set; }

        public string arrivalStation { get; set; } = null!;

        public string flightCarrier { get; set; } = null!;

        public int flightNumber { get; set; }

        public int price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVuelos.DAL.DTO
{
    public class TransportDto
    {
        public int Id { get; set; }

        public string? FlightCarrier { get; set; }

        public string? FlightNumber { get; set; }
    }
}

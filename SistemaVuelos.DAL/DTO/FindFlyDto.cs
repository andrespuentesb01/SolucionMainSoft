using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVuelos.DAL.DTO
{
    public class FindFlyDto
    {
        public string Origin { get; set; } = null!;

        public string Destination { get; set; } = null!;
    }
}

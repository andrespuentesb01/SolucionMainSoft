using SistemaVuelos.DAL.DTONewShore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVuelos.BLL.Services
{
    internal class FlightService
    {
        public FlightService() { }

        public List<FlightNewShoreDto> VuelosFor(List<FlightNewShoreDto> flightsFilter, List<FlightNewShoreDto> flights)
        {
            List<FlightNewShoreDto> flightResponse = new List<FlightNewShoreDto>(); 
            FlightNewShoreDto flightFilter = new FlightNewShoreDto();
            foreach (var i in flights)
            {
                foreach (var ii in flightsFilter)
                {

                    if (i.departureStation == ii.arrivalStation)
                    {
                        flightFilter.departureStation = ii.arrivalStation;
                        flightFilter.arrivalStation = i.arrivalStation;
                        flightResponse.Add(flightFilter);
                        //caminoTemp = caminoTemp + data.Origin + "-" + data.Destination + " \\ ";
                      

                    }
                }
            }
            return flightResponse;

        }
    }
}

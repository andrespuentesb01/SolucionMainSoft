using System;
using System.Collections.Generic;

namespace SistemaVuelos.Entity;

public partial class TblTransport
{
    public int Id { get; set; }

    public string? FlightCarrier { get; set; }

    public string? FlightNumber { get; set; }
}

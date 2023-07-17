using System;
using System.Collections.Generic;

namespace SistemaVuelos.Entity;

public partial class TblFlight
{
    public int Id { get; set; }

    public int IdTransport { get; set; }

    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public double Price { get; set; }
}

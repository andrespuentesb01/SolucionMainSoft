using System;
using System.Collections.Generic;

namespace SistemaVuelos.Entity;

public partial class TblJourney
{
    public int Id { get; set; }

    public int IdFlight { get; set; }

    public DateTime Date { get; set; }
}

using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class Disponibility
{
    public int Id { get; set; }

    public int? IdCar { get; set; }

    public int? IdDelivery { get; set; }

    public int? IdCollect { get; set; }
}

using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class Rent
{
    public int Id { get; set; }

    public int? IdCar { get; set; }

    public int? IdDelivery { get; set; }

    public int? IdCollect { get; set; }

    public int? IdStatus { get; set; }

    public string Comments { get; set; } = null!;

    public double? Value { get; set; }

    public int? IdUser { get; set; }
}

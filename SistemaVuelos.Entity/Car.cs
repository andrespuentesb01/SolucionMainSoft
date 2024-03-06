using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class Car
{
    public int Id { get; set; }

    public string? Branch { get; set; }

    public string? Model { get; set; }

    public string? Year { get; set; }

    public string? Plate { get; set; }
}

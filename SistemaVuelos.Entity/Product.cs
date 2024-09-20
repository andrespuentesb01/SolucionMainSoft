using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class Product
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int? IvaPercent { get; set; }

    public int? Stock { get; set; }
}

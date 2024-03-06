using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class Delivery
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int? IdSite { get; set; }
}

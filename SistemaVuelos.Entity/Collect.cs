using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class Collect
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public int? IdSite { get; set; }
}

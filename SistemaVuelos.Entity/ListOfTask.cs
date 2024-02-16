using System;
using System.Collections.Generic;

namespace SlnPactia.Domain;

public partial class ListOfTask
{
    public int Id { get; set; }

    public string NameOfTask { get; set; } = null!;

    public string Status { get; set; } = null!;
}

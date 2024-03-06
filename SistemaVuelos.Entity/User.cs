using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Cc { get; set; }

    public string? DrivePermision { get; set; }
}

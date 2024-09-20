using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class OrderHeader
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public decimal SubTotal { get; set; }

    public decimal IvaHeader { get; set; }

    public decimal Total { get; set; }
}

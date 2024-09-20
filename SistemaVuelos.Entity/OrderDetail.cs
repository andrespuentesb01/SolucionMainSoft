using System;
using System.Collections.Generic;

namespace SlnMain.Domain;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int IdHeader { get; set; }

    public int IdProduct { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }

    public decimal IvaDetail { get; set; }

    public decimal TotalDetail { get; set; }
}

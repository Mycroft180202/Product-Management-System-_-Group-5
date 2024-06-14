using System;
using System.Collections.Generic;

namespace Product_Management_System.Models;

public partial class ProductInventory
{
    public int ProductId { get; set; }

    public short LocationId { get; set; }

    public string Shelf { get; set; } = null!;

    public byte Bin { get; set; }

    public short Quantity { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

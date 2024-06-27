using System;
using System.Collections.Generic;

namespace Product_Management_System.Models;

public partial class Location
{
    public short LocationId { get; set; }

    public string Name { get; set; } = null!;

    public decimal CostRate { get; set; }

    public decimal Availability { get; set; }

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
}

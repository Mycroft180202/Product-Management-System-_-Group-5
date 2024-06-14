using System;
using System.Collections.Generic;

namespace Product_Management_System.Models;

public partial class ProductCostHistory
{
    public int ProductId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal Cost { get; set; }

    public virtual Product Product { get; set; } = null!;
}

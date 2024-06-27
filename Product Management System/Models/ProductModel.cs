using System;
using System.Collections.Generic;

namespace Product_Management_System.Models;

public partial class ProductModel
{
    public int ModelId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

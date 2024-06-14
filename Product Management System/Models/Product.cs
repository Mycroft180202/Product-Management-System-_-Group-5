using System;
using System.Collections.Generic;

namespace Product_Management_System.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public decimal Cost { get; set; }

    public decimal Price { get; set; }

    public int? SubcategoryId { get; set; }

    public int? ModelId { get; set; }

    public DateTime SellStartDate { get; set; }

    public DateTime? SellEndDate { get; set; }

    public virtual ProductModel? Model { get; set; }

    public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; } = new List<ProductCostHistory>();

    public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; set; } = new List<ProductPriceHistory>();

    public virtual ProductSubcategory? Subcategory { get; set; }
}

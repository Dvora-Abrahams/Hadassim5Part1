using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Good
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public double Price { get; set; }

    

    public int MinimumPurcheQuantity { get; set; }

    public virtual ICollection<GoodsToOrder> GoodsToOrders { get; set; } = new List<GoodsToOrder>();

    public virtual ICollection<GoodsToSupplier> GoodsToSuppliers { get; set; } = new List<GoodsToSupplier>();
}

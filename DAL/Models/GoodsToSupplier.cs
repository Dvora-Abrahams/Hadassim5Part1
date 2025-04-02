using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GoodsToSupplier
{
    public int Id { get; set; }

    public int SuppliersId { get; set; }

    public int GoodsId { get; set; }

    public virtual Good Goods { get; set; } = null!;

    public virtual Supplier Suppliers { get; set; } = null!;
}

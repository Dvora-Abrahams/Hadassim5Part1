using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GoodsToOrder
{
    public int Id { get; set; }

    public int GoodsId { get; set; }

    public int OrdersId { get; set; }
    public int Quantity { get; set; }
    public virtual Good Goods { get; set; } = null!;

    public virtual Order Orders { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Status { get; set; } = "waiting"!;

    public int SupplierId { get; set; }
    public int OrderQuantityNum { get; set; } = 0;
    [NotMapped]
    public virtual List<Good> goods { get; set; } = new List<Good>();
  
    public double FinalPrice { get; set; } = 0;
    public virtual ICollection<GoodsToOrder> GoodsToOrders { get; set; } = new List<GoodsToOrder>();

    public virtual Supplier Supplier { get; set; } = null!;
}

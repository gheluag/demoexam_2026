using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Orderstatus
{
    public int IdStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Manufacrurer
{
    public int IdManuf { get; set; }

    public string ManufName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

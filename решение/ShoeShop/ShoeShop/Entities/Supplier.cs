using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Supplier
{
    public int IdSup { get; set; }

    public string SupName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

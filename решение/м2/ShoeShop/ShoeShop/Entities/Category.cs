using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Category
{
    public int IdCat { get; set; }

    public string CatName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

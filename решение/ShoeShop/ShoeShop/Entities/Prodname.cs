using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Prodname
{
    public int IdProdName { get; set; }

    public string ProdName1 { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

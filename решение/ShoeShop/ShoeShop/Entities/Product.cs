using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Product
{
    public int IdProd { get; set; }

    public string Article { get; set; } = null!;

    public int ProdName { get; set; }

    public decimal Price { get; set; }

    public int Sale { get; set; }

    public int Count { get; set; }

    public string? Descrip { get; set; }

    public string? Image { get; set; }

    public int SupId { get; set; }

    public int ManufId { get; set; }

    public int CatId { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual Manufacrurer Manuf { get; set; } = null!;

    public virtual Prodname ProdNameNavigation { get; set; } = null!;

    public virtual ICollection<Prodorder> Prodorders { get; set; } = new List<Prodorder>();

    public virtual Supplier Sup { get; set; } = null!;




    public bool HasDiscount => Sale > 0;

    public bool IsBigDiscount => Sale > 15;

    public bool IsOutOfStock => Count == 0;

    public decimal FinalPrice =>
        Sale > 0 ? Price * (100 - Sale) / 100 : Price;


}

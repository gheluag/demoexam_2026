using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class Order
{
    public int IdOrder { get; set; }

    public DateOnly DateOrder { get; set; }

    public DateOnly? DateDelivery { get; set; }

    public int? Codes { get; set; }

    public int? PvzId { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<Prodorder> Prodorders { get; set; } = new List<Prodorder>();

    public virtual Pvz? Pvz { get; set; }

    public virtual Orderstatus? Status { get; set; }

    public virtual ICollection<Usersorder> Usersorders { get; set; } = new List<Usersorder>();
}

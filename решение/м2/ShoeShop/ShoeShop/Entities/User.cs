using System;
using System.Collections.Generic;

namespace ShoeShop.Entities;

public partial class User
{
    public int IdUser { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Passw { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Usersrole Role { get; set; } = null!;

    public virtual ICollection<Usersorder> Usersorders { get; set; } = new List<Usersorder>();
}

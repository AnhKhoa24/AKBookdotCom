using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Tacgium
{
    public int MaTg { get; set; }

    public string? TenTg { get; set; }

    public string? Diachi { get; set; }

    public string? Tieusu { get; set; }

    public string? Dienthoai { get; set; }

    public virtual ICollection<Vietsac> Vietsacs { get; set; } = new List<Vietsac>();
}

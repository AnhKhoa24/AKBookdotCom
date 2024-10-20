using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Role
{
    public long Id { get; set; }

    public string? TenQuyen { get; set; }

    public virtual ICollection<Khachhang> Khachhangs { get; set; } = new List<Khachhang>();
}

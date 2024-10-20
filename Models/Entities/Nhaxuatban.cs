using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Nhaxuatban
{
    public int MaNxb { get; set; }

    public string? TenNxb { get; set; }

    public string? Diachi { get; set; }

    public string? Dienthoai { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}

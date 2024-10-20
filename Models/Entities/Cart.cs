using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Cart
{
    public int Id { get; set; }

    public int? MaKh { get; set; }

    public int? MaSach { get; set; }

    public decimal? Gia { get; set; }

    public int? Sl { get; set; }

    public decimal? Tongtien { get; set; }

    public virtual Khachhang? MaKhNavigation { get; set; }

    public virtual Sach? MaSachNavigation { get; set; }
}

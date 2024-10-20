using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Chitietdondathang
{
    public int? MaDonHang { get; set; }

    public int? Masach { get; set; }

    public int? Soluong { get; set; }

    public decimal? Dongia { get; set; }

    public int Id { get; set; }

    public virtual Dondathang? MaDonHangNavigation { get; set; }

    public virtual Sach? MasachNavigation { get; set; }
}

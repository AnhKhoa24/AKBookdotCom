using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Dondathang
{
    public int MaDonHang { get; set; }

    public DateOnly? Ngaydat { get; set; }

    public DateOnly? Ngaygiao { get; set; }

    public string? Tinhtranggiaohang { get; set; }

    public int? MaKh { get; set; }

    public virtual ICollection<Chitietdondathang> Chitietdondathangs { get; set; } = new List<Chitietdondathang>();

    public virtual Khachhang? MaKhNavigation { get; set; }
}

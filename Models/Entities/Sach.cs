using System;
using System.Collections.Generic;

namespace AKBookdotCom.Models.Entities;

public partial class Sach
{
    public int Masach { get; set; }

    public string? Tensach { get; set; }

    public decimal? Giaban { get; set; }

    public string? Mota { get; set; }

    public string? Anhbia { get; set; }

    public DateOnly? Ngaycapnhat { get; set; }

    public int? Soluongton { get; set; }

    public int? MaCd { get; set; }

    public int? MaNxb { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Chitietdondathang> Chitietdondathangs { get; set; } = new List<Chitietdondathang>();

    public virtual Chude? MaCdNavigation { get; set; }

    public virtual Nhaxuatban? MaNxbNavigation { get; set; }

    public virtual ICollection<Vietsac> Vietsacs { get; set; } = new List<Vietsac>();
}

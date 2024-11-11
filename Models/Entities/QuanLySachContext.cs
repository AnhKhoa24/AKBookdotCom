using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AKBookdotCom.Models.Entities;

public partial class QuanLySachContext : DbContext
{
    public QuanLySachContext()
    {
    }

    public QuanLySachContext(DbContextOptions<QuanLySachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Chitietdondathang> Chitietdondathangs { get; set; }

    public virtual DbSet<Chude> Chudes { get; set; }

    public virtual DbSet<Dondathang> Dondathangs { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Nhaxuatban> Nhaxuatbans { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<Tacgium> Tacgia { get; set; }

    public virtual DbSet<Vietsac> Vietsacs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=SachThayHuynh;Integrated Security=True");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Vietnamese_CI_AS");

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gia)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gia");
            entity.Property(e => e.MaKh).HasColumnName("maKH");
            entity.Property(e => e.MaSach).HasColumnName("ma_sach");
            entity.Property(e => e.Sl).HasColumnName("sl");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tongtien");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_Cart_KHACHHANG");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.MaSach)
                .HasConstraintName("FK_Cart_SACH");
        });

        modelBuilder.Entity<Chitietdondathang>(entity =>
        {
            entity.ToTable("CHITIETDONDATHANG");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dongia).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.MaDonHangNavigation).WithMany(p => p.Chitietdondathangs)
                .HasForeignKey(d => d.MaDonHang)
                .HasConstraintName("FK__CHITIETDO__MaDon__49C3F6B7");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.Chitietdondathangs)
                .HasForeignKey(d => d.Masach)
                .HasConstraintName("FK__CHITIETDO__Masac__4AB81AF0");
        });

        modelBuilder.Entity<Chude>(entity =>
        {
            entity.HasKey(e => e.MaCd).HasName("PK__CHUDE__27258E045CFC35D7");

            entity.ToTable("CHUDE");

            entity.Property(e => e.MaCd).HasColumnName("MaCD");
            entity.Property(e => e.TenChuDe).HasMaxLength(100);
        });

        modelBuilder.Entity<Dondathang>(entity =>
        {
            entity.HasKey(e => e.MaDonHang).HasName("PK__DONDATHA__129584AD7995AF20");

            entity.ToTable("DONDATHANG");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Tinhtranggiaohang).HasMaxLength(50);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__DONDATHANG__MaKH__46E78A0C");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KHACHHAN__2725CF1ED0F5F8D1");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.DiachiKh)
                .HasMaxLength(250)
                .HasColumnName("DiachiKH");
            entity.Property(e => e.DienthoaiKh)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DienthoaiKH");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.IdQuyen).HasColumnName("idQuyen");
            entity.Property(e => e.Matkhau).HasMaxLength(250);
            entity.Property(e => e.Taikhoan)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdQuyenNavigation).WithMany(p => p.Khachhangs)
                .HasForeignKey(d => d.IdQuyen)
                .HasConstraintName("FK_KHACHHANG_Roles");
        });

        modelBuilder.Entity<Nhaxuatban>(entity =>
        {
            entity.HasKey(e => e.MaNxb).HasName("PK__NHAXUATB__3A19482C8DD17D33");

            entity.ToTable("NHAXUATBAN");

            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.Diachi).HasMaxLength(250);
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenNxb)
                .HasMaxLength(100)
                .HasColumnName("TenNXB");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TenQuyen).HasMaxLength(50);
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__SACH__9F923C887E623695");

            entity.ToTable("SACH");

            entity.Property(e => e.Anhbia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Giaban).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MaCd).HasColumnName("MaCD");
            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.Tensach).HasMaxLength(255);

            entity.HasOne(d => d.MaCdNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaCd)
                .HasConstraintName("FK__SACH__MaCD__3D5E1FD2");

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaNxb)
                .HasConstraintName("FK__SACH__MaNXB__3E52440B");
        });

        modelBuilder.Entity<Tacgium>(entity =>
        {
            entity.HasKey(e => e.MaTg).HasName("PK__TACGIA__2725007405DD9063");

            entity.ToTable("TACGIA");

            entity.Property(e => e.MaTg).HasColumnName("MaTG");
            entity.Property(e => e.Diachi).HasMaxLength(50);
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TenTg)
                .HasMaxLength(100)
                .HasColumnName("TenTG");
            entity.Property(e => e.Tieusu).HasMaxLength(250);
        });

        modelBuilder.Entity<Vietsac>(entity =>
        {
            entity.HasKey(e => new { e.MaTg, e.Masach }).HasName("PK__VIETSAC__BEDC23BC85009A38");

            entity.ToTable("VIETSAC");

            entity.Property(e => e.MaTg).HasColumnName("MaTG");
            entity.Property(e => e.Vaitro).HasMaxLength(50);
            entity.Property(e => e.Vitri).HasMaxLength(50);

            entity.HasOne(d => d.MaTgNavigation).WithMany(p => p.Vietsacs)
                .HasForeignKey(d => d.MaTg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VIETSAC__MaTG__412EB0B6");

            entity.HasOne(d => d.MasachNavigation).WithMany(p => p.Vietsacs)
                .HasForeignKey(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VIETSAC__Masach__4222D4EF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

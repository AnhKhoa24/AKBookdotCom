USE [BookStore]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[maKH] [int] NULL,
	[ma_sach] [int] NULL,
	[gia] [decimal](10, 2) NULL,
	[sl] [int] NULL,
	[tongtien] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHITIETDONDATHANG]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHITIETDONDATHANG](
	[MaDonHang] [int] NULL,
	[Masach] [int] NULL,
	[Soluong] [int] NULL,
	[Dongia] [decimal](10, 2) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CHITIETDONDATHANG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHUDE]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHUDE](
	[MaCD] [int] IDENTITY(1,1) NOT NULL,
	[TenChuDe] [nvarchar](100) NULL,
 CONSTRAINT [PK__CHUDE__27258E045CFC35D7] PRIMARY KEY CLUSTERED 
(
	[MaCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DONDATHANG]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DONDATHANG](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[Ngaydat] [date] NULL,
	[Ngaygiao] [date] NULL,
	[Tinhtranggiaohang] [nvarchar](50) NULL,
	[MaKH] [int] NULL,
 CONSTRAINT [PK__DONDATHA__129584AD7995AF20] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KHACHHANG]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](100) NULL,
	[Taikhoan] [varchar](50) NULL,
	[Matkhau] [nvarchar](250) NULL,
	[Email] [varchar](100) NULL,
	[DiachiKH] [nvarchar](250) NULL,
	[DienthoaiKH] [varchar](20) NULL,
	[Ngaysinh] [date] NULL,
	[idQuyen] [bigint] NULL,
 CONSTRAINT [PK__KHACHHAN__2725CF1ED0F5F8D1] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHAXUATBAN]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHAXUATBAN](
	[MaNXB] [int] IDENTITY(1,1) NOT NULL,
	[TenNXB] [nvarchar](100) NULL,
	[Diachi] [nvarchar](250) NULL,
	[Dienthoai] [varchar](20) NULL,
 CONSTRAINT [PK__NHAXUATB__3A19482C8DD17D33] PRIMARY KEY CLUSTERED 
(
	[MaNXB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[TenQuyen] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SACH]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SACH](
	[Masach] [int] IDENTITY(1,1) NOT NULL,
	[Tensach] [nvarchar](255) NULL,
	[Giaban] [decimal](10, 2) NULL,
	[Mota] [nvarchar](max) NULL,
	[Anhbia] [varchar](255) NULL,
	[Ngaycapnhat] [date] NULL,
	[Soluongton] [int] NULL,
	[MaCD] [int] NULL,
	[MaNXB] [int] NULL,
 CONSTRAINT [PK__SACH__9F923C887E623695] PRIMARY KEY CLUSTERED 
(
	[Masach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TACGIA]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TACGIA](
	[MaTG] [int] IDENTITY(1,1) NOT NULL,
	[TenTG] [nvarchar](100) NULL,
	[Diachi] [nvarchar](50) NULL,
	[Tieusu] [nvarchar](250) NULL,
	[Dienthoai] [varchar](20) NULL,
 CONSTRAINT [PK__TACGIA__2725007405DD9063] PRIMARY KEY CLUSTERED 
(
	[MaTG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VIETSAC]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VIETSAC](
	[MaTG] [int] NOT NULL,
	[Masach] [int] NOT NULL,
	[Vaitro] [nvarchar](50) NULL,
	[Vitri] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTG] ASC,
	[Masach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_KHACHHANG] FOREIGN KEY([maKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_KHACHHANG]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_SACH] FOREIGN KEY([ma_sach])
REFERENCES [dbo].[SACH] ([Masach])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_SACH]
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK__CHITIETDO__MaDon__49C3F6B7] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DONDATHANG] ([MaDonHang])
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG] CHECK CONSTRAINT [FK__CHITIETDO__MaDon__49C3F6B7]
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK__CHITIETDO__Masac__4AB81AF0] FOREIGN KEY([Masach])
REFERENCES [dbo].[SACH] ([Masach])
GO
ALTER TABLE [dbo].[CHITIETDONDATHANG] CHECK CONSTRAINT [FK__CHITIETDO__Masac__4AB81AF0]
GO
ALTER TABLE [dbo].[DONDATHANG]  WITH CHECK ADD  CONSTRAINT [FK__DONDATHANG__MaKH__46E78A0C] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KHACHHANG] ([MaKH])
GO
ALTER TABLE [dbo].[DONDATHANG] CHECK CONSTRAINT [FK__DONDATHANG__MaKH__46E78A0C]
GO
ALTER TABLE [dbo].[KHACHHANG]  WITH CHECK ADD  CONSTRAINT [FK_KHACHHANG_Roles] FOREIGN KEY([idQuyen])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[KHACHHANG] CHECK CONSTRAINT [FK_KHACHHANG_Roles]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK__SACH__MaCD__3D5E1FD2] FOREIGN KEY([MaCD])
REFERENCES [dbo].[CHUDE] ([MaCD])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK__SACH__MaCD__3D5E1FD2]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK__SACH__MaNXB__3E52440B] FOREIGN KEY([MaNXB])
REFERENCES [dbo].[NHAXUATBAN] ([MaNXB])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK__SACH__MaNXB__3E52440B]
GO
ALTER TABLE [dbo].[VIETSAC]  WITH CHECK ADD  CONSTRAINT [FK__VIETSAC__Masach__4222D4EF] FOREIGN KEY([Masach])
REFERENCES [dbo].[SACH] ([Masach])
GO
ALTER TABLE [dbo].[VIETSAC] CHECK CONSTRAINT [FK__VIETSAC__Masach__4222D4EF]
GO
ALTER TABLE [dbo].[VIETSAC]  WITH CHECK ADD  CONSTRAINT [FK__VIETSAC__MaTG__412EB0B6] FOREIGN KEY([MaTG])
REFERENCES [dbo].[TACGIA] ([MaTG])
GO
ALTER TABLE [dbo].[VIETSAC] CHECK CONSTRAINT [FK__VIETSAC__MaTG__412EB0B6]
GO
/****** Object:  StoredProcedure [dbo].[ThongKeDonHang]    Script Date: 03/08/2025 10:47:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ThongKeDonHang]
    @NgayBatDau NVARCHAR(10), 
    @NgayKetThuc NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Chuyển đổi đầu vào sang kiểu DATE
    DECLARE @StartDate DATE = CONVERT(DATE, @NgayBatDau, 103);
    DECLARE @EndDate DATE = CONVERT(DATE, @NgayKetThuc, 103);

    -- Tạo danh sách ngày sử dụng Table Variable (Tối ưu hơn CTE đệ quy)
    DECLARE @Ngay TABLE (Ngay DATE PRIMARY KEY);
    
    DECLARE @NgayTam DATE = @StartDate;
    WHILE @NgayTam <= @EndDate
    BEGIN
        INSERT INTO @Ngay (Ngay) VALUES (@NgayTam);
        SET @NgayTam = DATEADD(DAY, 1, @NgayTam);
    END

    -- Thống kê số lượng đơn theo ngày
    SELECT 
        n.Ngay, 
        ISNULL(SUM(CASE WHEN d.MaDonHang IS NOT NULL THEN 1 ELSE 0 END), 0) AS SoLuongDon
    FROM @Ngay n
    LEFT JOIN DONDATHANG d ON CONVERT(DATE, d.NgayDat) = n.Ngay
    GROUP BY n.Ngay
    ORDER BY n.Ngay;
END
GO

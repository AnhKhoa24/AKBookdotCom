﻿CREATE TABLE Roles (
    ID BIGINT PRIMARY KEY IDENTITY(1,1), 
    TenQuyen NVARCHAR(50) NOT NULL,     
);


CREATE TABLE KHACHHANG (
    MaKH BIGINT PRIMARY KEY IDENTITY(1,1),  
    HoTen NVARCHAR(200),
    TaiKhoan NVARCHAR(50),
    MatKhau NVARCHAR(250),
    Email NVARCHAR(100),
    DiaChiKH NVARCHAR(250),
    DienThoaiKH NVARCHAR(11),
    NgaySinh DATE,
    idQuyen BIGINT FOREIGN KEY REFERENCES Roles(ID) 
);

CREATE TABLE CHUDE (
    MaCD BIGINT PRIMARY KEY IDENTITY(1,1), 
    TenChuDe NVARCHAR(50) NOT NULL,     
);

CREATE TABLE NHAXUATBAN (
    MaNXB BIGINT PRIMARY KEY IDENTITY(1,1), 
    TenNXB NVARCHAR(200),
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(12)
);

CREATE TABLE TACGIA (
    MaTG BIGINT PRIMARY KEY IDENTITY(1,1), 
    TenTG NVARCHAR(200),
    DiaChi NVARCHAR(200),
    TieuSu NVARCHAR(MAX),
    DienThoai NVARCHAR(12)
);

CREATE TABLE SACH (
    Masach BIGINT PRIMARY KEY IDENTITY(1,1), 
    Tensach NVARCHAR(200),
    Giaban FLOAT,
    Mota NVARCHAR(MAX),
    Anhbia NVARCHAR(250),
    Ngaycapnhat DATE,
    Soluongton BIGINT,
    MaCD BIGINT FOREIGN KEY REFERENCES CHUDE(MaCD) 
);
CREATE TABLE VIETSACH (
   MaTG BIGINT NOT NULL,
   Masach BIGINT NOT NULL,
   PRIMARY KEY (MaTG, Masach),
   Vaitro NVARCHAR(50),
   Vitri NVARCHAR(50),
   FOREIGN KEY (MaTG) REFERENCES TACGIA(MaTG),
   FOREIGN KEY (Masach) REFERENCES SACH(Masach)
);

CREATE TABLE DONDATHANG (
    MaDonHang BIGINT PRIMARY KEY IDENTITY(1,1), 
    Ngaydat DATE,
    Ngaygiao DATE,
    Tinhtranggiaohang NVARCHAR(50),
    MaKH BIGINT FOREIGN KEY REFERENCES KHACHHANG(MaKH) 
);

CREATE TABLE DONDATHANG (
    id BIGINT PRIMARY KEY IDENTITY(1,1), 
    Soluong BIGINT,
    MaDonHang BIGINT FOREIGN KEY REFERENCES DONDATHANG(MaDonHang),
    Masach BIGINT FOREIGN KEY REFERENCES SACH(Masach) 
);

CREATE TABLE Cart (
    id BIGINT PRIMARY KEY IDENTITY(1,1), 
    sl BIGINT,
    gia FLOAT,
    tongtien FLOAT,
    maKH BIGINT FOREIGN KEY REFERENCES KHACHHANG(MaKH),
    ma_sach BIGINT FOREIGN KEY REFERENCES SACH(Masach) 
);
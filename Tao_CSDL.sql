CREATE DATABASE CSDL_DatVeXemPhim;
GO

USE CSDL_DatVeXemPhim;
GO
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    SoDienThoai VARCHAR(15),
    VaiTro NVARCHAR(30) NOT NULL DEFAULT N'KhachHang',
    NgayTao DATETIME DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1
);
CREATE TABLE Phim (
    MaPhim INT IDENTITY(1,1) PRIMARY KEY,
    TenPhim NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX),
    DaoDien NVARCHAR(150),
    DienVien NVARCHAR(MAX),
    ThoiLuong INT NOT NULL,
    NgayKhoiChieu DATE,
    NgayKetThuc DATE,
    QuocGia NVARCHAR(100),
    DoTuoi NVARCHAR(20),
    Poster NVARCHAR(500),
    Trailer NVARCHAR(500),
    TrangThai NVARCHAR(30) DEFAULT N'SapChieu'
);

CREATE TABLE TheLoai (
    MaTheLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenTheLoai NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Phim_TheLoai (
    MaPhim INT NOT NULL,
    MaTheLoai INT NOT NULL,

    PRIMARY KEY (MaPhim, MaTheLoai),

    FOREIGN KEY (MaPhim) REFERENCES Phim(MaPhim),
    FOREIGN KEY (MaTheLoai) REFERENCES TheLoai(MaTheLoai)
);
CREATE TABLE RapChieu (
    MaRap INT IDENTITY(1,1) PRIMARY KEY,
    TenRap NVARCHAR(150) NOT NULL,
    DiaChi NVARCHAR(300),
    SoDienThoai VARCHAR(15),
    TrangThai BIT DEFAULT 1
);

CREATE TABLE PhongChieu (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    MaRap INT NOT NULL,
    TenPhong NVARCHAR(100) NOT NULL,
    LoaiPhong NVARCHAR(50),
    SoLuongGhe INT,

    FOREIGN KEY (MaRap) REFERENCES RapChieu(MaRap),
    UNIQUE (MaRap, TenPhong)
);

CREATE TABLE GheNgoi (
    MaGhe INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong INT NOT NULL,
    HangGhe CHAR(2) NOT NULL,
    SoGhe INT NOT NULL,
    LoaiGhe NVARCHAR(30) DEFAULT N'Thuong',
    TrangThai BIT DEFAULT 1,

    FOREIGN KEY (MaPhong) REFERENCES PhongChieu(MaPhong),
    UNIQUE (MaPhong, HangGhe, SoGhe)
);
CREATE TABLE SuatChieu (
    MaSuatChieu INT IDENTITY(1,1) PRIMARY KEY,
    MaPhim INT NOT NULL,
    MaPhong INT NOT NULL,
    NgayChieu DATE NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    GiaVe DECIMAL(12,2) NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'DangMoBan',

    FOREIGN KEY (MaPhim) REFERENCES Phim(MaPhim),
    FOREIGN KEY (MaPhong) REFERENCES PhongChieu(MaPhong),

    CHECK (GiaVe >= 0),
    CHECK (GioKetThuc > GioBatDau)
);
CREATE TABLE GheSuatChieu (
    MaGheSuatChieu INT IDENTITY(1,1) PRIMARY KEY,
    MaSuatChieu INT NOT NULL,
    MaGhe INT NOT NULL,
    GiaVe DECIMAL(12,2) NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'Trong',
    ThoiGianHetHan DATETIME NULL,

    FOREIGN KEY (MaSuatChieu) REFERENCES SuatChieu(MaSuatChieu),
    FOREIGN KEY (MaGhe) REFERENCES GheNgoi(MaGhe),

    UNIQUE (MaSuatChieu, MaGhe),

    CHECK (GiaVe >= 0),
    CHECK (TrangThai IN (N'Trong', N'DangGiu', N'DaDat'))
);
CREATE TABLE DatVe (
    MaDatVe INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaSuatChieu INT NOT NULL,
    NgayDat DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(12,2) NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'ChoThanhToan',

    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaSuatChieu) REFERENCES SuatChieu(MaSuatChieu),

    CHECK (TongTien >= 0),
    CHECK (TrangThai IN (
        N'ChoThanhToan',
        N'DaThanhToan',
        N'DaHuy',
        N'HetHan'
    ))
);

CREATE TABLE ChiTietDatVe (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaDatVe INT NOT NULL,
    MaGheSuatChieu INT NOT NULL,
    DonGia DECIMAL(12,2) NOT NULL,

    FOREIGN KEY (MaDatVe) REFERENCES DatVe(MaDatVe),
    FOREIGN KEY (MaGheSuatChieu) REFERENCES GheSuatChieu(MaGheSuatChieu),

    UNIQUE (MaGheSuatChieu),
    CHECK (DonGia >= 0)
);
CREATE TABLE ThanhToan (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    MaDatVe INT NOT NULL,
    PhuongThuc NVARCHAR(50) NOT NULL,
    MaGiaoDich VARCHAR(150),
    SoTien DECIMAL(12,2) NOT NULL,
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) DEFAULT N'ChoXuLy',

    FOREIGN KEY (MaDatVe) REFERENCES DatVe(MaDatVe),

    CHECK (SoTien >= 0),
    CHECK (TrangThai IN (
        N'ChoXuLy',
        N'ThanhCong',
        N'ThatBai',
        N'HoanTien'
    ))
);
CREATE TABLE VeDienTu (
    MaVe INT IDENTITY(1,1) PRIMARY KEY,
    MaChiTiet INT NOT NULL UNIQUE,
    MaQR VARCHAR(200) NOT NULL UNIQUE,
    NgayPhatHanh DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) DEFAULT N'ChuaSuDung',
    NgaySuDung DATETIME NULL,

    FOREIGN KEY (MaChiTiet) REFERENCES ChiTietDatVe(MaChiTiet),

    CHECK (TrangThai IN (
        N'ChuaSuDung',
        N'DaSuDung',
        N'DaHuy'
    ))
);
CREATE TABLE ThongBao (
    MaThongBao INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    TieuDe NVARCHAR(200) NOT NULL,
    NoiDung NVARCHAR(MAX),
    NgayGui DATETIME DEFAULT GETDATE(),
    DaDoc BIT DEFAULT 0,

    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);
CREATE TABLE DanhGiaPhim (
    MaDanhGia INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NOT NULL,
    MaPhim INT NOT NULL,
    SoSao INT NOT NULL,
    BinhLuan NVARCHAR(1000),
    NgayDanhGia DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaPhim) REFERENCES Phim(MaPhim),

    CHECK (SoSao BETWEEN 1 AND 5),
    UNIQUE (MaNguoiDung, MaPhim)
);

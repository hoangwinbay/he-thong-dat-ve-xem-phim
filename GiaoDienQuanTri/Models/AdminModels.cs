using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GiaoDienQuanTri.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }
        [Required, MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;
        [Required, MaxLength(150), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(255)]
        public string MatKhau { get; set; } = string.Empty;
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }
        [Required, MaxLength(30)]
        public string VaiTro { get; set; } = "KhachHang";
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public bool TrangThai { get; set; } = true;
    }

    [Table("Phim")]
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }
        [Required, MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        [MaxLength(150)]
        public string? DaoDien { get; set; }
        public string? DienVien { get; set; }
        public int ThoiLuong { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayKhoiChieu { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayKetThuc { get; set; }
        [MaxLength(100)]
        public string? QuocGia { get; set; }
        [MaxLength(20)]
        public string? DoTuoi { get; set; }
        [MaxLength(500)]
        public string? Poster { get; set; }
        [MaxLength(500)]
        public string? Trailer { get; set; }
        [MaxLength(30)]
        public string TrangThai { get; set; } = "SapChieu";

        public virtual ICollection<PhimTheLoai> PhimTheLoais { get; set; } = new List<PhimTheLoai>();
        public virtual ICollection<SuatChieu> SuatChieus { get; set; } = new List<SuatChieu>();
    }

    [Table("TheLoai")]
    public class TheLoai
    {
        [Key]
        public int MaTheLoai { get; set; }
        [Required, MaxLength(100)]
        public string TenTheLoai { get; set; } = string.Empty;
        public virtual ICollection<PhimTheLoai> PhimTheLoais { get; set; } = new List<PhimTheLoai>();
    }

    [Table("Phim_TheLoai")]
    public class PhimTheLoai
    {
        public int MaPhim { get; set; }
        public virtual Phim? Phim { get; set; }
        public int MaTheLoai { get; set; }
        public virtual TheLoai? TheLoai { get; set; }
    }

    [Table("RapChieu")]
    public class RapChieu
    {
        [Key]
        public int MaRap { get; set; }
        [Required, MaxLength(150)]
        public string TenRap { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? DiaChi { get; set; }
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }
        public bool TrangThai { get; set; } = true;
        public virtual ICollection<PhongChieu> PhongChieus { get; set; } = new List<PhongChieu>();
    }

    [Table("PhongChieu")]
    public class PhongChieu
    {
        [Key]
        public int MaPhong { get; set; }
        [Required]
        public int MaRap { get; set; }
        public virtual RapChieu? RapChieu { get; set; }
        [Required, MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? LoaiPhong { get; set; }
        public int? SoLuongGhe { get; set; }
        public virtual ICollection<GheNgoi> GheNgois { get; set; } = new List<GheNgoi>();
        public virtual ICollection<SuatChieu> SuatChieus { get; set; } = new List<SuatChieu>();
    }

    [Table("GheNgoi")]
    public class GheNgoi
    {
        [Key]
        public int MaGhe { get; set; }
        [Required]
        public int MaPhong { get; set; }
        public virtual PhongChieu? PhongChieu { get; set; }
        [Required, MaxLength(2)]
        public string HangGhe { get; set; } = string.Empty;
        [Required]
        public int SoGhe { get; set; }
        [MaxLength(30)]
        public string LoaiGhe { get; set; } = "Thuong";
        public bool TrangThai { get; set; } = true;
    }

    [Table("SuatChieu")]
    public class SuatChieu
    {
        [Key]
        public int MaSuatChieu { get; set; }
        [Required]
        public int MaPhim { get; set; }
        public virtual Phim? Phim { get; set; }
        [Required]
        public int MaPhong { get; set; }
        public virtual PhongChieu? PhongChieu { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal GiaVe { get; set; }
        [MaxLength(30)]
        public string TrangThai { get; set; } = "DangMoBan";
        public virtual ICollection<GheSuatChieu> GheSuatChieus { get; set; } = new List<GheSuatChieu>();
    }

    [Table("GheSuatChieu")]
    public class GheSuatChieu
    {
        [Key]
        public int MaGheSuatChieu { get; set; }
        [Required]
        public int MaSuatChieu { get; set; }
        public virtual SuatChieu? SuatChieu { get; set; }
        [Required]
        public int MaGhe { get; set; }
        public virtual GheNgoi? GheNgoi { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal GiaVe { get; set; }
        [MaxLength(30)]
        public string TrangThai { get; set; } = "Trong";
        public DateTime? ThoiGianHetHan { get; set; }
    }

    [Table("DatVe")]
    public class DatVe
    {
        [Key]
        public int MaDatVe { get; set; }
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public int MaSuatChieu { get; set; }
        public virtual SuatChieu? SuatChieu { get; set; }
        public DateTime NgayDat { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(12,2)")]
        public decimal TongTien { get; set; }
        [MaxLength(30)]
        public string TrangThai { get; set; } = "ChoThanhToan";
        public virtual ICollection<ChiTietDatVe> ChiTietDatVes { get; set; } = new List<ChiTietDatVe>();
        public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
    }

    [Table("ChiTietDatVe")]
    public class ChiTietDatVe
    {
        [Key]
        public int MaChiTiet { get; set; }
        public int MaDatVe { get; set; }
        public virtual DatVe? DatVe { get; set; }
        public int MaGheSuatChieu { get; set; }
        public virtual GheSuatChieu? GheSuatChieu { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal DonGia { get; set; }
        public virtual VeDienTu? VeDienTu { get; set; }
    }

    [Table("VeDienTu")]
    public class VeDienTu
    {
        [Key]
        public int MaVe { get; set; }
        public int MaChiTiet { get; set; }
        public virtual ChiTietDatVe? ChiTietDatVe { get; set; }
        [Required, MaxLength(200)]
        public string MaQR { get; set; } = string.Empty;
        public DateTime NgayPhatHanh { get; set; } = DateTime.Now;
        [MaxLength(30)]
        public string TrangThai { get; set; } = "ChuaSuDung"; // ChuaSuDung, DaSuDung, DaHuy
        public DateTime? NgaySuDung { get; set; }
    }

    [Table("ThanhToan")]
    public class ThanhToan
    {
        [Key]
        public int MaThanhToan { get; set; }
        public int MaDatVe { get; set; }
        public virtual DatVe? DatVe { get; set; }
        [Required, MaxLength(50)]
        public string PhuongThuc { get; set; } = string.Empty;
        [MaxLength(150)]
        public string? MaGiaoDich { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal SoTien { get; set; }
        public DateTime NgayThanhToan { get; set; } = DateTime.Now;
        [MaxLength(30)]
        public string TrangThai { get; set; } = "ChoXuLy";
    }

    [Table("ThongBao")]
    public class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        [Required, MaxLength(200)]
        public string TieuDe { get; set; } = string.Empty;
        public string? NoiDung { get; set; }
        public DateTime NgayGui { get; set; } = DateTime.Now;
        public bool DaDoc { get; set; } = false;
    }

    [Table("DanhGiaPhim")]
    public class DanhGiaPhim
    {
        [Key]
        public int MaDanhGia { get; set; }
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public int MaPhim { get; set; }
        public virtual Phim? Phim { get; set; }
        [Range(1, 5)]
        public int SoSao { get; set; }
        [MaxLength(1000)]
        public string? BinhLuan { get; set; }
        public DateTime NgayDanhGia { get; set; } = DateTime.Now;
    }
}

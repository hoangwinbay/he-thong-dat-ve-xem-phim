using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuQuanLyVeDienTu.Models
{
    [Table("VeDienTu")]
    public class VeDienTu
    {
        [Key]
        public int MaVe { get; set; }

        [Required]
        public int MaChiTiet { get; set; }
        public virtual ChiTietDatVe? ChiTietDatVe { get; set; }

        [Required]
        [MaxLength(200)]
        public string MaQR { get; set; } = string.Empty;

        public DateTime NgayPhatHanh { get; set; } = DateTime.Now;

        [MaxLength(30)]
        public string TrangThai { get; set; } = "ChuaSuDung"; // ChuaSuDung, DaSuDung, DaHuy

        public DateTime? NgaySuDung { get; set; }
    }

    [Table("ChiTietDatVe")]
    public class ChiTietDatVe
    {
        [Key]
        public int MaChiTiet { get; set; }

        [Required]
        public int MaDatVe { get; set; }
        public virtual DatVe? DatVe { get; set; }

        [Required]
        public int MaGheSuatChieu { get; set; }
        public virtual GheSuatChieu? GheSuatChieu { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal DonGia { get; set; }

        public virtual VeDienTu? VeDienTu { get; set; }
    }

    [Table("DatVe")]
    public class DatVe
    {
        [Key]
        public int MaDatVe { get; set; }

        [Required]
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }

        [Required]
        public int MaSuatChieu { get; set; }
        public virtual SuatChieu? SuatChieu { get; set; }

        public DateTime NgayDat { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(12,2)")]
        public decimal TongTien { get; set; }

        [MaxLength(30)]
        public string TrangThai { get; set; } = "ChoThanhToan";

        public virtual ICollection<ChiTietDatVe> ChiTietDatVes { get; set; } = new List<ChiTietDatVe>();
    }

    [Table("GheSuatChieu")]
    public class GheSuatChieu
    {
        [Key]
        public int MaGheSuatChieu { get; set; }

        public int MaSuatChieu { get; set; }

        public int MaGhe { get; set; }
        public virtual GheNgoi? GheNgoi { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal GiaVe { get; set; }

        [MaxLength(30)]
        public string TrangThai { get; set; } = "Trong";
    }

    [Table("GheNgoi")]
    public class GheNgoi
    {
        [Key]
        public int MaGhe { get; set; }

        public int MaPhong { get; set; }

        [Required]
        [MaxLength(2)]
        public string HangGhe { get; set; } = string.Empty;

        public int SoGhe { get; set; }

        [MaxLength(30)]
        public string LoaiGhe { get; set; } = "Thuong";
    }

    [Table("SuatChieu")]
    public class SuatChieu
    {
        [Key]
        public int MaSuatChieu { get; set; }

        public int MaPhim { get; set; }
        public virtual Phim? Phim { get; set; }

        public int MaPhong { get; set; }
        public virtual PhongChieu? PhongChieu { get; set; }

        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal GiaVe { get; set; }
    }

    [Table("Phim")]
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Poster { get; set; }

        [MaxLength(20)]
        public string? DoTuoi { get; set; }

        public int ThoiLuong { get; set; }
    }

    [Table("PhongChieu")]
    public class PhongChieu
    {
        [Key]
        public int MaPhong { get; set; }

        public int MaRap { get; set; }
        public virtual RapChieu? RapChieu { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LoaiPhong { get; set; }
    }

    [Table("RapChieu")]
    public class RapChieu
    {
        [Key]
        public int MaRap { get; set; }

        [Required]
        [MaxLength(150)]
        public string TenRap { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? DiaChi { get; set; }
    }

    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }

        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? SoDienThoai { get; set; }
    }
}

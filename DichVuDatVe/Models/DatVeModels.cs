using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuDatVe.Models
{
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
        public string TrangThai { get; set; } = "ChoThanhToan"; // ChoThanhToan, DaThanhToan, DaHuy, HetHan

        public virtual ICollection<ChiTietDatVe> ChiTietDatVes { get; set; } = new List<ChiTietDatVe>();
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
        public string TrangThai { get; set; } = "Trong"; // Trong, DangGiu, DaDat

        public DateTime? ThoiGianHetHan { get; set; }
    }

    [Table("GheNgoi")]
    public class GheNgoi
    {
        [Key]
        public int MaGhe { get; set; }

        [Required]
        public int MaPhong { get; set; }

        [Required]
        [MaxLength(2)]
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

        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal GiaVe { get; set; }

        [MaxLength(30)]
        public string TrangThai { get; set; } = "DangMoBan";
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

        public int ThoiLuong { get; set; }
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
    }

    [Table("RapChieu")]
    public class RapChieu
    {
        [Key]
        public int MaRap { get; set; }

        [Required]
        [MaxLength(150)]
        public string TenRap { get; set; } = string.Empty;
    }
}

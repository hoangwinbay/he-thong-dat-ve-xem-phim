using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuQuanLySuatChieu.Models
{
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
        public string TrangThai { get; set; } = "DangMoBan"; // DangMoBan, HetVe, DaChieu, Huy
    }

    [Table("Phim")]
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;

        public int ThoiLuong { get; set; }
        public string? Poster { get; set; }
        public string? DoTuoi { get; set; }
    }

    [Table("PhongChieu")]
    public class PhongChieu
    {
        [Key]
        public int MaPhong { get; set; }

        public int MaRap { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LoaiPhong { get; set; }

        public int? SoLuongGhe { get; set; }
    }
}


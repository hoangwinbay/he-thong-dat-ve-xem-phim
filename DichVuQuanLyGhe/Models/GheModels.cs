using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuQuanLyGhe.Models
{
    [Table("GheNgoi")]
    public class GheNgoi
    {
        [Key]
        public int MaGhe { get; set; }

        [Required]
        public int MaPhong { get; set; }
        public virtual PhongChieu? PhongChieu { get; set; }

        [Required]
        [MaxLength(2)]
        public string HangGhe { get; set; } = string.Empty;

        [Required]
        public int SoGhe { get; set; }

        [MaxLength(30)]
        public string LoaiGhe { get; set; } = "Thuong"; // Thuong, VIP, Doi

        public bool TrangThai { get; set; } = true;
    }

    [Table("GheSuatChieu")]
    public class GheSuatChieu
    {
        [Key]
        public int MaGheSuatChieu { get; set; }

        [Required]
        public int MaSuatChieu { get; set; }

        [Required]
        public int MaGhe { get; set; }
        public virtual GheNgoi? GheNgoi { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal GiaVe { get; set; }

        [MaxLength(30)]
        public string TrangThai { get; set; } = "Trong"; // Trong, DangGiu, DaDat

        public DateTime? ThoiGianHetHan { get; set; }
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


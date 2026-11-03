using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuQuanLyRap.Models
{
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

        [Required]
        [MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LoaiPhong { get; set; } // 2D, 3D, IMAX, 4DX, ScreenX, VIP

        public int? SoLuongGhe { get; set; }
    }
}


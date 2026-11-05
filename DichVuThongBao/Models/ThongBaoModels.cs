using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuThongBao.Models
{
    [Table("ThongBao")]
    public class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        [Required]
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }

        [Required]
        [MaxLength(200)]
        public string TieuDe { get; set; } = string.Empty;

        public string? NoiDung { get; set; }

        public DateTime NgayGui { get; set; } = DateTime.Now;

        public bool DaDoc { get; set; } = false;
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
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? SoDienThoai { get; set; }

        [MaxLength(20)]
        public string VaiTro { get; set; } = "KhachHang";

        public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuDanhGiaPhim.Models
{
    [Table("DanhGiaPhim")]
    public class DanhGiaPhim
    {
        [Key]
        public int MaDanhGia { get; set; }

        [Required]
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }

        [Required]
        public int MaPhim { get; set; }
        public virtual Phim? Phim { get; set; }

        [Range(1, 5, ErrorMessage = "Số sao đánh giá phải từ 1 đến 5 sao")]
        public int SoSao { get; set; }

        [MaxLength(1000)]
        public string? BinhLuan { get; set; }

        public DateTime NgayDanhGia { get; set; } = DateTime.Now;
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
    }

    [Table("Phim")]
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;

        public string? Poster { get; set; }

        public int ThoiLuong { get; set; }

        [MaxLength(20)]
        public string? DoTuoi { get; set; }
    }
}

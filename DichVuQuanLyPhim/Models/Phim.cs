using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DichVuQuanLyPhim.Models
{
    [Table("Phim")]
    public class Phim
    {
        [Key]
        public int MaPhim { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;

        public string? MoTa { get; set; }

        [MaxLength(150)]
        public string? DaoDien { get; set; }

        public string? DienVien { get; set; }

        [Required]
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
        public string TrangThai { get; set; } = "SapChieu"; // DangChieu, SapChieu, NgungChieu

        public virtual ICollection<PhimTheLoai> PhimTheLoais { get; set; } = new List<PhimTheLoai>();
    }

    [Table("TheLoai")]
    public class TheLoai
    {
        [Key]
        public int MaTheLoai { get; set; }

        [Required]
        [MaxLength(100)]
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
}


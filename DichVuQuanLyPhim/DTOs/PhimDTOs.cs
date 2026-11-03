using System.ComponentModel.DataAnnotations;

namespace DichVuQuanLyPhim.DTOs
{
    public class ThemPhimDTO
    {
        [Required(ErrorMessage = "Tên phim không được để trống")]
        [MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;

        public string? MoTa { get; set; }

        [MaxLength(150)]
        public string? DaoDien { get; set; }

        public string? DienVien { get; set; }

        [Required(ErrorMessage = "Thời lượng phim là bắt buộc")]
        [Range(1, 600, ErrorMessage = "Thời lượng từ 1 đến 600 phút")]
        public int ThoiLuong { get; set; }

        public DateTime? NgayKhoiChieu { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        [MaxLength(100)]
        public string? QuocGia { get; set; }

        [MaxLength(20)]
        public string? DoTuoi { get; set; } // P, K, T13, T16, T18

        [MaxLength(500)]
        public string? Poster { get; set; }

        [MaxLength(500)]
        public string? Trailer { get; set; }

        public string TrangThai { get; set; } = "SapChieu"; // DangChieu, SapChieu, NgungChieu

        public List<int>? TheLoaiIds { get; set; }
    }

    public class CapNhatPhimDTO
    {
        [Required(ErrorMessage = "Tên phim không được để trống")]
        [MaxLength(200)]
        public string TenPhim { get; set; } = string.Empty;

        public string? MoTa { get; set; }

        [MaxLength(150)]
        public string? DaoDien { get; set; }

        public string? DienVien { get; set; }

        [Required]
        public int ThoiLuong { get; set; }

        public DateTime? NgayKhoiChieu { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        [MaxLength(100)]
        public string? QuocGia { get; set; }

        [MaxLength(20)]
        public string? DoTuoi { get; set; }

        [MaxLength(500)]
        public string? Poster { get; set; }

        [MaxLength(500)]
        public string? Trailer { get; set; }

        public string TrangThai { get; set; } = "SapChieu";

        public List<int>? TheLoaiIds { get; set; }
    }

    public class CapNhatTrangThaiDTO
    {
        [Required]
        public string TrangThai { get; set; } = string.Empty; // DangChieu, SapChieu, NgungChieu
    }

    public class ThongTinPhimDTO
    {
        public int MaPhim { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? MoTa { get; set; }
        public string? DaoDien { get; set; }
        public string? DienVien { get; set; }
        public int ThoiLuong { get; set; }
        public DateTime? NgayKhoiChieu { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string? QuocGia { get; set; }
        public string? DoTuoi { get; set; }
        public string? Poster { get; set; }
        public string? Trailer { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public List<string> TheLoai { get; set; } = new();
    }
}


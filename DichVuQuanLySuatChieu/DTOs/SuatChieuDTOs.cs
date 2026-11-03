using System.ComponentModel.DataAnnotations;

namespace DichVuQuanLySuatChieu.DTOs
{
    public class ThemSuatChieuDTO
    {
        [Required(ErrorMessage = "Mã phim là bắt buộc")]
        public int MaPhim { get; set; }

        [Required(ErrorMessage = "Mã phòng chiếu là bắt buộc")]
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Ngày chiếu là bắt buộc")]
        public DateTime NgayChieu { get; set; }

        [Required(ErrorMessage = "Giờ bắt đầu là bắt buộc")]
        public TimeSpan GioBatDau { get; set; }

        [Required(ErrorMessage = "Giờ kết thúc là bắt buộc")]
        public TimeSpan GioKetThuc { get; set; }

        [Required(ErrorMessage = "Giá vé là bắt buộc")]
        [Range(0, 10000000, ErrorMessage = "Giá vé phải lớn hơn hoặc bằng 0")]
        public decimal GiaVe { get; set; }

        public string TrangThai { get; set; } = "DangMoBan";
    }

    public class CapNhatSuatChieuDTO
    {
        [Required]
        public int MaPhim { get; set; }

        [Required]
        public int MaPhong { get; set; }

        [Required]
        public DateTime NgayChieu { get; set; }

        [Required]
        public TimeSpan GioBatDau { get; set; }

        [Required]
        public TimeSpan GioKetThuc { get; set; }

        [Required]
        [Range(0, 10000000)]
        public decimal GiaVe { get; set; }

        public string TrangThai { get; set; } = "DangMoBan";
    }

    public class ThongTinSuatChieuDTO
    {
        public int MaSuatChieu { get; set; }
        public int MaPhim { get; set; }
        public string? TenPhim { get; set; }
        public string? PosterPhim { get; set; }
        public string? DoTuoi { get; set; }
        public int ThoiLuong { get; set; }
        public int MaPhong { get; set; }
        public string? TenPhong { get; set; }
        public string? LoaiPhong { get; set; }
        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal GiaVe { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }

    public class LocSuatChieuDTO
    {
        public int? MaPhim { get; set; }
        public int? MaPhong { get; set; }
        public DateTime? NgayChieu { get; set; }
        public string? TrangThai { get; set; }
    }
}


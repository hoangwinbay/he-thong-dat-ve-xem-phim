using System.ComponentModel.DataAnnotations;

namespace DichVuQuanLyVeDienTu.DTOs
{
    public class KiemTraVeRequestDTO
    {
        [Required(ErrorMessage = "Mã QR hoặc mã vé không được để trống")]
        public string MaQR { get; set; } = string.Empty;
    }

    public class KetQuaSoatVeDTO
    {
        public bool HopLe { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public int MaVe { get; set; }
        public string MaQR { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
        public DateTime? NgaySuDung { get; set; }

        public int MaDatVe { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? PosterPhim { get; set; }
        public string TenRap { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public string ViTriGhe { get; set; } = string.Empty;
        public string LoaiGhe { get; set; } = string.Empty;
        public decimal GiaVe { get; set; }
    }

    public class VeDienTuDTO
    {
        public int MaVe { get; set; }
        public int MaChiTiet { get; set; }
        public int MaDatVe { get; set; }
        public string MaQR { get; set; } = string.Empty;
        public DateTime NgayPhatHanh { get; set; }
        public string TrangThai { get; set; } = string.Empty; // ChuaSuDung, DaSuDung, DaHuy
        public DateTime? NgaySuDung { get; set; }

        public string TenKhachHang { get; set; } = string.Empty;
        public string EmailKhachHang { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }

        public string TenPhim { get; set; } = string.Empty;
        public string? PosterPhim { get; set; }
        public string? DoTuoi { get; set; }
        public int ThoiLuong { get; set; }

        public string TenRap { get; set; } = string.Empty;
        public string? DiaChiRap { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        public string HangGhe { get; set; } = string.Empty;
        public int SoGhe { get; set; }
        public string ViTriGhe => $"{HangGhe}{SoGhe}";
        public string LoaiGhe { get; set; } = string.Empty;
        public decimal GiaVe { get; set; }
    }
}

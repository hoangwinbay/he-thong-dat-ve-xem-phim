using System.ComponentModel.DataAnnotations;

namespace DichVuThanhToan.DTOs
{
    public class TaoGiaoDichThanhToanDTO
    {
        [Required(ErrorMessage = "Mã đặt vé không được để trống")]
        public int MaDatVe { get; set; }

        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public string PhuongThuc { get; set; } = "VNPAY"; // VNPAY, MOMO, TheQuocTe, TienMatTaiRap

        public decimal? SoTien { get; set; }
    }

    public class CapNhatTrangThaiThanhToanDTO
    {
        [Required]
        public string TrangThai { get; set; } = string.Empty; // ChoXuLy, ThanhCong, ThatBai, HoanTien

        public string? MaGiaoDich { get; set; }
    }

    public class ThanhToanDTO
    {
        public int MaThanhToan { get; set; }
        public int MaDatVe { get; set; }
        public string PhuongThuc { get; set; } = string.Empty;
        public string? MaGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // Thông tin đơn vé liên quan
        public string TenKhachHang { get; set; } = string.Empty;
        public string EmailKhachHang { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? PosterPhim { get; set; }
        public string TenRap { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public List<string> DanhSachGhe { get; set; } = new();
    }

    public class KetQuaThanhToanDTO
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
        public int MaThanhToan { get; set; }
        public string? MaGiaoDich { get; set; }
        public int MaDatVe { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; } = string.Empty;
        public DateTime NgayThanhToan { get; set; }
    }
}

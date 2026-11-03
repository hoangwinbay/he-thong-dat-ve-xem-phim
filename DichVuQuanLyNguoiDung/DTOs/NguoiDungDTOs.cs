using System.ComponentModel.DataAnnotations;

namespace DichVuQuanLyNguoiDung.DTOs
{
    public class DangKyDTO
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        public string MatKhau { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? SoDienThoai { get; set; }
    }

    public class DangNhapDTO
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; } = string.Empty;
    }

    public class CapNhatNguoiDungDTO
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        public bool? TrangThai { get; set; }
    }

    public class PhanQuyenDTO
    {
        [Required(ErrorMessage = "Vai trò không được để trống")]
        public string VaiTro { get; set; } = "KhachHang"; // QuanTri, NhanVien, KhachHang
    }

    public class DoiMatKhauDTO
    {
        [Required]
        public string MatKhauCu { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string MatKhauMoi { get; set; } = string.Empty;
    }

    public class ThongTinNguoiDungDTO
    {
        public int MaNguoiDung { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }
        public string VaiTro { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string? Token { get; set; }
    }
}


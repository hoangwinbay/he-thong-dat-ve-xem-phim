using System.ComponentModel.DataAnnotations;

namespace DichVuQuanLyRap.DTOs
{
    public class ThemRapDTO
    {
        [Required(ErrorMessage = "Tên rạp không được để trống")]
        [MaxLength(150)]
        public string TenRap { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? DiaChi { get; set; }

        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        public bool TrangThai { get; set; } = true;
    }

    public class CapNhatRapDTO
    {
        [Required(ErrorMessage = "Tên rạp không được để trống")]
        [MaxLength(150)]
        public string TenRap { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? DiaChi { get; set; }

        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        public bool TrangThai { get; set; } = true;
    }

    public class ThongTinRapDTO
    {
        public int MaRap { get; set; }
        public string TenRap { get; set; } = string.Empty;
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public bool TrangThai { get; set; }
        public int SoLuongPhongChieu { get; set; }
        public List<ThongTinPhongChieuDTO> PhongChieus { get; set; } = new();
    }

    public class ThemPhongChieuDTO
    {
        [Required]
        public int MaRap { get; set; }

        [Required(ErrorMessage = "Tên phòng chiếu không được để trống")]
        [MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LoaiPhong { get; set; } // 2D, 3D, IMAX, ScreenX, VIP

        public int? SoLuongGhe { get; set; }
    }

    public class CapNhatPhongChieuDTO
    {
        [Required(ErrorMessage = "Tên phòng chiếu không được để trống")]
        [MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LoaiPhong { get; set; }

        public int? SoLuongGhe { get; set; }
    }

    public class ThongTinPhongChieuDTO
    {
        public int MaPhong { get; set; }
        public int MaRap { get; set; }
        public string? TenRap { get; set; }
        public string TenPhong { get; set; } = string.Empty;
        public string? LoaiPhong { get; set; }
        public int? SoLuongGhe { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace DichVuDatVe.DTOs
{
    public class TaoDonDatVeDTO
    {
        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        public int MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Mã suất chiếu không được để trống")]
        public int MaSuatChieu { get; set; }

        [Required(ErrorMessage = "Danh sách ghế đặt không được để trống")]
        [MinLength(1, ErrorMessage = "Phải chọn ít nhất 1 ghế")]
        public List<int> DanhSachMaGheSuatChieu { get; set; } = new();
    }

    public class CapNhatTrangThaiDatVeDTO
    {
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; } = string.Empty; // ChoThanhToan, DaThanhToan, DaHuy, HetHan
    }

    public class ChiTietGheDatDTO
    {
        public int MaGheSuatChieu { get; set; }
        public int MaGhe { get; set; }
        public string HangGhe { get; set; } = string.Empty;
        public int SoGhe { get; set; }
        public string MaGheHienThi => $"{HangGhe}{SoGhe}";
        public string LoaiGhe { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
    }

    public class DonDatVeDTO
    {
        public int MaDatVe { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string EmailNguoiDung { get; set; } = string.Empty;
        public string? SoDienThoai { get; set; }

        public int MaSuatChieu { get; set; }
        public int MaPhim { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? PosterPhim { get; set; }
        public string TenRap { get; set; } = string.Empty;
        public string TenPhong { get; set; } = string.Empty;
        public DateTime NgayChieu { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        public List<ChiTietGheDatDTO> DanhSachGhe { get; set; } = new();
    }
}

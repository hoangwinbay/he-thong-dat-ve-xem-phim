using System.ComponentModel.DataAnnotations;

namespace DichVuQuanLyGhe.DTOs
{
    public class ThemGheDTO
    {
        [Required]
        public int MaPhong { get; set; }

        [Required]
        [MaxLength(2)]
        public string HangGhe { get; set; } = string.Empty;

        [Required]
        public int SoGhe { get; set; }

        public string LoaiGhe { get; set; } = "Thuong";
        public bool TrangThai { get; set; } = true;
    }

    public class CapNhatGheDTO
    {
        public string LoaiGhe { get; set; } = "Thuong";
        public bool TrangThai { get; set; } = true;
    }

    public class ThongTinGheDTO
    {
        public int MaGhe { get; set; }
        public int MaPhong { get; set; }
        public string HangGhe { get; set; } = string.Empty;
        public int SoGhe { get; set; }
        public string TenGhe => $"{HangGhe}{SoGhe}";
        public string LoaiGhe { get; set; } = string.Empty;
        public bool TrangThai { get; set; }
    }

    public class ThongTinGheSuatChieuDTO
    {
        public int MaGheSuatChieu { get; set; }
        public int MaSuatChieu { get; set; }
        public int MaGhe { get; set; }
        public string HangGhe { get; set; } = string.Empty;
        public int SoGhe { get; set; }
        public string TenGhe => $"{HangGhe}{SoGhe}";
        public string LoaiGhe { get; set; } = string.Empty;
        public decimal GiaVe { get; set; }
        public string TrangThai { get; set; } = string.Empty; // Trong, DangGiu, DaDat
        public DateTime? ThoiGianHetHan { get; set; }
    }

    public class SoDoGheSuatChieuDTO
    {
        public int MaSuatChieu { get; set; }
        public int TongSoGhe { get; set; }
        public int SoGheTrong { get; set; }
        public int SoGheDangGiu { get; set; }
        public int SoGheDaDat { get; set; }
        public List<ThongTinGheSuatChieuDTO> DanhSachGhe { get; set; } = new();
    }

    public class GiuGheRequestDTO
    {
        [Required]
        public int MaSuatChieu { get; set; }

        [Required]
        public List<int> DanhSachMaGheSuatChieu { get; set; } = new();

        public int SoPhutGiu { get; set; } = 10; // Giữ 10 phút mặc định
    }

    public class GiaiPhongGheRequestDTO
    {
        [Required]
        public int MaSuatChieu { get; set; }

        [Required]
        public List<int> DanhSachMaGheSuatChieu { get; set; } = new();
    }

    public class KhoiTaoGheSuatChieuDTO
    {
        [Required]
        public int MaSuatChieu { get; set; }

        [Required]
        public int MaPhong { get; set; }

        [Required]
        public decimal GiaVeCoBan { get; set; }

        public decimal PhuThuGheVIP { get; set; } = 20000;
        public decimal PhuThuGheDoi { get; set; } = 80000;
    }
}


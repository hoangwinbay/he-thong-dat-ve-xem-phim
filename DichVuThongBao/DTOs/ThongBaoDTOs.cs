using System.ComponentModel.DataAnnotations;

namespace DichVuThongBao.DTOs
{
    public class ThongBaoDTO
    {
        public int MaThongBao { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string EmailNguoiDung { get; set; } = string.Empty;
        public string TieuDe { get; set; } = string.Empty;
        public string? NoiDung { get; set; }
        public DateTime NgayGui { get; set; }
        public bool DaDoc { get; set; }
    }

    public class GuiThongBaoRequestDTO
    {
        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        public int MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Tiêu đề thông báo không được để trống")]
        [MaxLength(200)]
        public string TieuDe { get; set; } = string.Empty;

        public string? NoiDung { get; set; }
    }

    public class GuiThongBaoTatCaRequestDTO
    {
        [Required(ErrorMessage = "Tiêu đề thông báo không được để trống")]
        [MaxLength(200)]
        public string TieuDe { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nội dung thông báo không được để trống")]
        public string NoiDung { get; set; } = string.Empty;
    }
}

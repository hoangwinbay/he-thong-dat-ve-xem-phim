using System.ComponentModel.DataAnnotations;

namespace DichVuDanhGiaPhim.DTOs
{
    public class DanhGiaPhimDTO
    {
        public int MaDanhGia { get; set; }
        public int MaNguoiDung { get; set; }
        public string TenNguoiDung { get; set; } = string.Empty;
        public string EmailNguoiDung { get; set; } = string.Empty;

        public int MaPhim { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public string? PosterPhim { get; set; }

        public int SoSao { get; set; }
        public string? BinhLuan { get; set; }
        public DateTime NgayDanhGia { get; set; }
    }

    public class TaoDanhGiaRequestDTO
    {
        [Required(ErrorMessage = "Mã người dùng không được để trống")]
        public int MaNguoiDung { get; set; }

        [Required(ErrorMessage = "Mã phim không được để trống")]
        public int MaPhim { get; set; }

        [Range(1, 5, ErrorMessage = "Số sao đánh giá phải từ 1 đến 5 sao")]
        public int SoSao { get; set; }

        [MaxLength(1000, ErrorMessage = "Bình luận tối đa 1000 ký tự")]
        public string? BinhLuan { get; set; }
    }

    public class ThongKeDanhGiaPhimDTO
    {
        public int MaPhim { get; set; }
        public string TenPhim { get; set; } = string.Empty;
        public double DiemTrungBinh { get; set; }
        public int TongSoDanhGia { get; set; }
        public int SoSao5 { get; set; }
        public int SoSao4 { get; set; }
        public int SoSao3 { get; set; }
        public int SoSao2 { get; set; }
        public int SoSao1 { get; set; }
    }
}

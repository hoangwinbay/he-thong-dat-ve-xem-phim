using DichVuQuanLyGhe.DTOs;
using DichVuQuanLyGhe.Models;
using DichVuQuanLyGhe.Repositories;

namespace DichVuQuanLyGhe.Services
{
    public class GheService : IGheService
    {
        private readonly IGheRepository _repository;

        public GheService(IGheRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ThongTinGheDTO>> GetGheByPhongAsync(int maPhong)
        {
            var ghes = await _repository.GetGheByPhongAsync(maPhong);
            return ghes.Select(MapGheToDTO);
        }

        public async Task<ThongTinGheDTO> ThemGheAsync(ThemGheDTO dto)
        {
            var ghe = new GheNgoi
            {
                MaPhong = dto.MaPhong,
                HangGhe = dto.HangGhe.Trim().ToUpper(),
                SoGhe = dto.SoGhe,
                LoaiGhe = dto.LoaiGhe,
                TrangThai = dto.TrangThai
            };

            await _repository.AddGheAsync(ghe);
            return MapGheToDTO(ghe);
        }

        public async Task<bool> CapNhatGheAsync(int maGhe, CapNhatGheDTO dto)
        {
            var ghe = await _repository.GetGheByIdAsync(maGhe);
            if (ghe == null) return false;

            ghe.LoaiGhe = dto.LoaiGhe;
            ghe.TrangThai = dto.TrangThai;

            await _repository.UpdateGheAsync(ghe);
            return true;
        }

        public async Task<bool> XoaGheAsync(int maGhe)
        {
            var ghe = await _repository.GetGheByIdAsync(maGhe);
            if (ghe == null) return false;

            await _repository.DeleteGheAsync(maGhe);
            return true;
        }

        public async Task<int> TaoSoDoGheMacDinhAsync(int maPhong, int soHang = 6, int soGheMoiHang = 8)
        {
            char startChar = 'A';
            var ghes = new List<GheNgoi>();

            for (int r = 0; r < soHang; r++)
            {
                string hang = ((char)(startChar + r)).ToString();
                string loaiGhe = (r < 2) ? "Thuong" : (r < soHang - 1 ? "VIP" : "Doi");

                int count = (loaiGhe == "Doi") ? soGheMoiHang / 2 : soGheMoiHang;

                for (int c = 1; c <= count; c++)
                {
                    ghes.Add(new GheNgoi
                    {
                        MaPhong = maPhong,
                        HangGhe = hang,
                        SoGhe = c,
                        LoaiGhe = loaiGhe,
                        TrangThai = true
                    });
                }
            }

            await _repository.AddGheRangeAsync(ghes);
            return ghes.Count;
        }

        public async Task<SoDoGheSuatChieuDTO> GetSoDoGheSuatChieuAsync(int maSuatChieu)
        {
            var list = await _repository.GetGheBySuatChieuAsync(maSuatChieu);

            // Tự động kiểm tra và giải phóng ghế giữ đã quá hạn
            var now = DateTime.Now;
            var expired = list.Where(g => g.TrangThai == "DangGiu" && g.ThoiGianHetHan.HasValue && g.ThoiGianHetHan.Value < now).ToList();
            if (expired.Any())
            {
                foreach (var g in expired)
                {
                    g.TrangThai = "Trong";
                    g.ThoiGianHetHan = null;
                }
                await _repository.UpdateGheSuatChieuRangeAsync(expired);
            }

            var dtos = list.Select(MapGheSuatChieuToDTO).ToList();

            return new SoDoGheSuatChieuDTO
            {
                MaSuatChieu = maSuatChieu,
                TongSoGhe = dtos.Count,
                SoGheTrong = dtos.Count(g => g.TrangThai == "Trong"),
                SoGheDangGiu = dtos.Count(g => g.TrangThai == "DangGiu"),
                SoGheDaDat = dtos.Count(g => g.TrangThai == "DaDat"),
                DanhSachGhe = dtos
            };
        }

        public async Task<int> KhoiTaoGheSuatChieuAsync(KhoiTaoGheSuatChieuDTO dto)
        {
            var ghesPhong = await _repository.GetGheByPhongAsync(dto.MaPhong);
            var gheSuatChieuList = new List<GheSuatChieu>();

            foreach (var g in ghesPhong.Where(g => g.TrangThai))
            {
                decimal gia = dto.GiaVeCoBan;
                if (g.LoaiGhe == "VIP") gia += dto.PhuThuGheVIP;
                else if (g.LoaiGhe == "Doi") gia += dto.PhuThuGheDoi;

                gheSuatChieuList.Add(new GheSuatChieu
                {
                    MaSuatChieu = dto.MaSuatChieu,
                    MaGhe = g.MaGhe,
                    GiaVe = gia,
                    TrangThai = "Trong",
                    ThoiGianHetHan = null
                });
            }

            await _repository.AddGheSuatChieuRangeAsync(gheSuatChieuList);
            return gheSuatChieuList.Count;
        }

        public async Task<bool> GiuGheAsync(GiuGheRequestDTO dto)
        {
            var ghes = await _repository.GetGheSuatChieuByIdsAsync(dto.DanhSachMaGheSuatChieu);
            if (ghes.Count != dto.DanhSachMaGheSuatChieu.Count) return false;

            // Kiểm tra tất cả ghế phải ở trạng thái "Trong" hoặc đã hết hạn giữ
            var now = DateTime.Now;
            foreach (var g in ghes)
            {
                if (g.TrangThai == "DaDat") return false;
                if (g.TrangThai == "DangGiu" && g.ThoiGianHetHan.HasValue && g.ThoiGianHetHan.Value > now)
                {
                    return false; // Đang được người khác giữ
                }
            }

            // Tiến hành giữ ghế
            var expireTime = now.AddMinutes(dto.SoPhutGiu > 0 ? dto.SoPhutGiu : 10);
            foreach (var g in ghes)
            {
                g.TrangThai = "DangGiu";
                g.ThoiGianHetHan = expireTime;
            }

            await _repository.UpdateGheSuatChieuRangeAsync(ghes);
            return true;
        }

        public async Task<bool> GiaiPhongGheAsync(GiaiPhongGheRequestDTO dto)
        {
            var ghes = await _repository.GetGheSuatChieuByIdsAsync(dto.DanhSachMaGheSuatChieu);
            foreach (var g in ghes)
            {
                if (g.TrangThai == "DangGiu")
                {
                    g.TrangThai = "Trong";
                    g.ThoiGianHetHan = null;
                }
            }

            await _repository.UpdateGheSuatChieuRangeAsync(ghes);
            return true;
        }

        public async Task<bool> DatGheAsync(List<int> danhSachMaGheSuatChieu)
        {
            var ghes = await _repository.GetGheSuatChieuByIdsAsync(danhSachMaGheSuatChieu);
            if (ghes.Count != danhSachMaGheSuatChieu.Count) return false;

            foreach (var g in ghes)
            {
                g.TrangThai = "DaDat";
                g.ThoiGianHetHan = null;
            }

            await _repository.UpdateGheSuatChieuRangeAsync(ghes);
            return true;
        }

        private static ThongTinGheDTO MapGheToDTO(GheNgoi g)
        {
            return new ThongTinGheDTO
            {
                MaGhe = g.MaGhe,
                MaPhong = g.MaPhong,
                HangGhe = g.HangGhe,
                SoGhe = g.SoGhe,
                LoaiGhe = g.LoaiGhe,
                TrangThai = g.TrangThai
            };
        }

        private static ThongTinGheSuatChieuDTO MapGheSuatChieuToDTO(GheSuatChieu gs)
        {
            return new ThongTinGheSuatChieuDTO
            {
                MaGheSuatChieu = gs.MaGheSuatChieu,
                MaSuatChieu = gs.MaSuatChieu,
                MaGhe = gs.MaGhe,
                HangGhe = gs.GheNgoi?.HangGhe ?? "",
                SoGhe = gs.GheNgoi?.SoGhe ?? 0,
                LoaiGhe = gs.GheNgoi?.LoaiGhe ?? "Thuong",
                GiaVe = gs.GiaVe,
                TrangThai = gs.TrangThai,
                ThoiGianHetHan = gs.ThoiGianHetHan
            };
        }
    }
}


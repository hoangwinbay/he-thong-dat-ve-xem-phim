using GiaoDienKhachHang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;

namespace GiaoDienKhachHang.Controllers
{
    public class PhimController : Controller
    {
        private readonly CustomerDbContext _context;

        public PhimController(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DanhSach(string? trangThai, int? theLoaiId, string? search)
        {
            var query = _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                query = query.Where(p => p.TrangThai == trangThai);
            }

            if (theLoaiId.HasValue)
            {
                query = query.Where(p => p.PhimTheLoais.Any(pt => pt.MaTheLoai == theLoaiId.Value));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.TenPhim.Contains(search) || (p.DienVien != null && p.DienVien.Contains(search)));
            }

            ViewBag.TheLoais = await _context.TheLoais.OrderBy(t => t.TenTheLoai).ToListAsync();
            ViewBag.SelectedTrangThai = trangThai;
            ViewBag.SelectedTheLoai = theLoaiId;
            ViewBag.Search = search;

            var list = await query.OrderByDescending(p => p.MaPhim).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var phim = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .Include(p => p.SuatChieus)
                .ThenInclude(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .FirstOrDefaultAsync(p => p.MaPhim == id);

            if (phim == null) return NotFound();

            var suatChieus = phim.SuatChieus
                .Where(s => s.TrangThai == "DangMoBan")
                .OrderBy(s => s.NgayChieu)
                .ThenBy(s => s.GioBatDau)
                .ToList();

            ViewBag.SuatChieus = suatChieus;

            // Tải danh sách đánh giá phim (Dịch vụ Đánh giá phim)
            var danhGias = await _context.DanhGiaPhims
                .Include(d => d.NguoiDung)
                .Where(d => d.MaPhim == id)
                .OrderByDescending(d => d.NgayDanhGia)
                .ToListAsync();

            ViewBag.DanhGias = danhGias;
            ViewBag.TongSoDanhGia = danhGias.Count;
            ViewBag.DiemTrungBinh = danhGias.Count > 0 ? Math.Round(danhGias.Average(d => d.SoSao), 1) : 0;

            return View(phim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuiDanhGia(int maPhim, int soSao, string? binhLuan, string? email)
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            if (!maNguoiDung.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(email))
                {
                    var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email.Trim());
                    if (user != null)
                    {
                        maNguoiDung = user.MaNguoiDung;
                    }
                }
            }

            if (!maNguoiDung.HasValue)
            {
                TempData["Error"] = "Vui lòng đăng nhập hoặc nhập đúng email tài khoản đã đăng ký để gửi đánh giá phim.";
                return RedirectToAction(nameof(ChiTiet), new { id = maPhim });
            }

            if (soSao < 1 || soSao > 5)
            {
                TempData["Error"] = "Số sao đánh giá phải từ 1 đến 5 sao.";
                return RedirectToAction(nameof(ChiTiet), new { id = maPhim });
            }

            var existing = await _context.DanhGiaPhims.FirstOrDefaultAsync(d => d.MaNguoiDung == maNguoiDung.Value && d.MaPhim == maPhim);
            if (existing != null)
            {
                existing.SoSao = soSao;
                existing.BinhLuan = binhLuan;
                existing.NgayDanhGia = DateTime.Now;
                TempData["Success"] = "Đã cập nhật đánh giá của bạn cho phim này!";
            }
            else
            {
                _context.DanhGiaPhims.Add(new DanhGiaPhim
                {
                    MaNguoiDung = maNguoiDung.Value,
                    MaPhim = maPhim,
                    SoSao = soSao,
                    BinhLuan = binhLuan,
                    NgayDanhGia = DateTime.Now
                });
                TempData["Success"] = "Cảm ơn bạn đã gửi đánh giá cho phim!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ChiTiet), new { id = maPhim });
        }
    }
}


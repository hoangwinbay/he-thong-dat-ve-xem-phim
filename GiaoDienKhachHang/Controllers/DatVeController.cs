using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;
using GiaoDienKhachHang.Models;

namespace GiaoDienKhachHang.Controllers
{
    public class DatVeController : Controller
    {
        private readonly CustomerDbContext _context;

        public DatVeController(CustomerDbContext context)
        {
            _context = context;
        }

        // Bước 1: Chọn rạp
        public async Task<IActionResult> ChonRap(int? maPhim)
        {
            var raps = await _context.RapChieus
                .Where(r => r.TrangThai)
                .OrderBy(r => r.TenRap)
                .ToListAsync();

            if (maPhim.HasValue)
            {
                ViewBag.Phim = await _context.Phims.FindAsync(maPhim.Value);
            }

            ViewBag.SelectedPhim = maPhim;
            return View(raps);
        }

        // Bước 2: Chọn suất chiếu
        public async Task<IActionResult> ChonSuatChieu(int? maPhim, int? maRap, DateTime? ngayChieu)
        {
            var targetNgay = ngayChieu?.Date ?? DateTime.Today;

            var query = _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .Where(s => s.TrangThai == "DangMoBan");

            if (maPhim.HasValue) query = query.Where(s => s.MaPhim == maPhim.Value);
            if (maRap.HasValue) query = query.Where(s => s.PhongChieu!.MaRap == maRap.Value);

            var suatChieus = await query
                .OrderBy(s => s.NgayChieu)
                .ThenBy(s => s.GioBatDau)
                .ToListAsync();

            ViewBag.Phims = await _context.Phims.Where(p => p.TrangThai == "DangChieu").ToListAsync();
            ViewBag.Raps = await _context.RapChieus.Where(r => r.TrangThai).ToListAsync();
            ViewBag.SelectedPhim = maPhim;
            ViewBag.SelectedRap = maRap;
            ViewBag.SelectedNgay = targetNgay;

            return View(suatChieus);
        }

        // Bước 3: Chọn ghế
        public async Task<IActionResult> ChonGhe(int id)
        {
            var suatChieu = await _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .Include(s => s.GheSuatChieus)
                .ThenInclude(gs => gs.GheNgoi)
                .FirstOrDefaultAsync(s => s.MaSuatChieu == id);

            if (suatChieu == null) return NotFound();

            return View(suatChieu);
        }

        // Bước 4: Xác nhận thông tin đặt vé
        [HttpGet]
        public async Task<IActionResult> XacNhanDatVe(int id, string gheIds)
        {
            var suatChieu = await _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .ThenInclude(pc => pc!.RapChieu)
                .FirstOrDefaultAsync(s => s.MaSuatChieu == id);

            if (suatChieu == null) return NotFound();

            var idList = (gheIds ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s.Trim(), out var parsed) ? parsed : 0)
                .Where(i => i > 0)
                .ToList();

            if (!idList.Any())
            {
                TempData["Error"] = "Vui lòng chọn ít nhất một ghế để tiếp tục đặt vé!";
                return RedirectToAction(nameof(ChonGhe), new { id });
            }

            var ghes = await _context.GheSuatChieus
                .Include(g => g.GheNgoi)
                .Where(g => idList.Contains(g.MaGheSuatChieu))
                .ToListAsync();

            ViewBag.SuatChieu = suatChieu;
            ViewBag.SelectedGhes = ghes;
            ViewBag.TongTien = ghes.Sum(g => g.GiaVe);
            ViewBag.GheIds = string.Join(",", idList);

            // Kiểm tra thông tin người dùng đang đăng nhập
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            if (maNguoiDung.HasValue)
            {
                var user = await _context.NguoiDungs.FindAsync(maNguoiDung.Value);
                ViewBag.CurrentUser = user;
            }

            return View();
        }

        // Bước 5: Thực hiện tạo đơn đặt vé
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaoDonDatVe(int maSuatChieu, string gheIds, string hoTen, string email, string? soDienThoai)
        {
            var idList = (gheIds ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.TryParse(s.Trim(), out var parsed) ? parsed : 0)
                .Where(i => i > 0)
                .ToList();

            if (!idList.Any())
            {
                TempData["Error"] = "Chưa có ghế nào được chọn!";
                return RedirectToAction(nameof(ChonGhe), new { id = maSuatChieu });
            }

            var ghes = await _context.GheSuatChieus
                .Include(g => g.GheNgoi)
                .Where(g => idList.Contains(g.MaGheSuatChieu))
                .ToListAsync();

            // Kiểm tra ghế có bị trùng đặt
            var unavailable = ghes.Where(g => g.TrangThai != "Trong").ToList();
            if (unavailable.Any())
            {
                var names = string.Join(", ", unavailable.Select(u => $"{u.GheNgoi?.HangGhe}{u.GheNgoi?.SoGhe}"));
                TempData["Error"] = $"Rất tiếc! Ghế {names} vừa có người khác giữ chỗ hoặc đặt. Vui lòng chọn ghế khác.";
                return RedirectToAction(nameof(ChonGhe), new { id = maSuatChieu });
            }

            // Xác định người dùng đặt vé
            int maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung") ?? 0;
            if (maNguoiDung == 0)
            {
                // Tìm kiếm tài khoản qua email hoặc tự động tạo tài khoản khách
                var existingUser = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
                if (existingUser != null)
                {
                    maNguoiDung = existingUser.MaNguoiDung;
                }
                else
                {
                    var newUser = new NguoiDung
                    {
                        HoTen = string.IsNullOrWhiteSpace(hoTen) ? "Khách hàng" : hoTen,
                        Email = string.IsNullOrWhiteSpace(email) ? $"khach_{DateTime.Now.Ticks}@cinebooking.vn" : email,
                        MatKhau = "123456",
                        SoDienThoai = soDienThoai,
                        VaiTro = "KhachHang",
                        NgayTao = DateTime.Now,
                        TrangThai = true
                    };
                    _context.NguoiDungs.Add(newUser);
                    await _context.SaveChangesAsync();
                    maNguoiDung = newUser.MaNguoiDung;
                }
            }

            // Tạo đơn đặt vé mới
            var donDatVe = new DatVe
            {
                MaNguoiDung = maNguoiDung,
                MaSuatChieu = maSuatChieu,
                NgayDat = DateTime.Now,
                TongTien = ghes.Sum(g => g.GiaVe),
                TrangThai = "ChoThanhToan"
            };

            foreach (var g in ghes)
            {
                donDatVe.ChiTietDatVes.Add(new ChiTietDatVe
                {
                    MaGheSuatChieu = g.MaGheSuatChieu,
                    DonGia = g.GiaVe
                });

                // Đổi trạng thái sang giữ chỗ
                g.TrangThai = "DangGiu";
                g.ThoiGianHetHan = DateTime.Now.AddMinutes(10);
            }

            _context.DatVes.Add(donDatVe);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"🎉 Đặt vé thành công! Mã đơn đặt vé: #{donDatVe.MaDatVe}. Vui lòng thanh toán trong vòng 10 phút.";
            return RedirectToAction("Index", "ThanhToan", new { maDatVe = donDatVe.MaDatVe });
        }
    }
}


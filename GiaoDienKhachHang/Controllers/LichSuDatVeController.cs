using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;

namespace GiaoDienKhachHang.Controllers
{
    public class LichSuDatVeController : Controller
    {
        private readonly CustomerDbContext _context;

        public LichSuDatVeController(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? emailTraCuu)
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");

            var query = _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .AsQueryable();

            if (maNguoiDung.HasValue)
            {
                query = query.Where(dv => dv.MaNguoiDung == maNguoiDung.Value);
            }
            else if (!string.IsNullOrWhiteSpace(emailTraCuu))
            {
                query = query.Where(dv => dv.NguoiDung != null && dv.NguoiDung.Email == emailTraCuu.Trim());
                ViewBag.EmailTraCuu = emailTraCuu;
            }
            else
            {
                // Mặc định lấy các đơn gần nhất để khách hàng theo dõi ngay sau khi vừa đặt
                query = query.Take(10);
            }

            var donDatVes = await query.OrderByDescending(dv => dv.NgayDat).ToListAsync();
            return View(donDatVes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyDon(int id)
        {
            var donDatVe = await _context.DatVes
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == id);

            if (donDatVe == null)
            {
                TempData["Error"] = "Không tìm thấy đơn đặt vé cần hủy.";
                return RedirectToAction(nameof(Index));
            }

            if (donDatVe.TrangThai != "ChoThanhToan")
            {
                TempData["Error"] = "Chỉ có thể hủy đơn đặt vé đang ở trạng thái 'Chờ thanh toán'.";
                return RedirectToAction(nameof(Index));
            }

            donDatVe.TrangThai = "DaHuy";

            // Giải phóng các ghế về trạng thái Trong
            foreach (var ct in donDatVe.ChiTietDatVes)
            {
                if (ct.GheSuatChieu != null)
                {
                    ct.GheSuatChieu.TrangThai = "Trong";
                    ct.GheSuatChieu.ThoiGianHetHan = null;
                }
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Đã hủy thành công đơn đặt vé #{id} và giải phóng ghế.";
            return RedirectToAction(nameof(Index));
        }
    }
}

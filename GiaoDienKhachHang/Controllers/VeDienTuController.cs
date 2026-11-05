using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;
using GiaoDienKhachHang.Models;

namespace GiaoDienKhachHang.Controllers
{
    public class VeDienTuController : Controller
    {
        private readonly CustomerDbContext _context;

        public VeDienTuController(CustomerDbContext context)
        {
            _context = context;
        }

        // GET: /VeDienTu/DonDatVe?maDatVe=1
        public async Task<IActionResult> DonDatVe(int maDatVe)
        {
            var datVe = await _context.DatVes
                .Include(dv => dv.NguoiDung)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.Phim)
                .Include(dv => dv.SuatChieu)
                    .ThenInclude(sc => sc!.PhongChieu)
                        .ThenInclude(pc => pc!.RapChieu)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .Include(dv => dv.ChiTietDatVes)
                    .ThenInclude(ct => ct.VeDienTu)
                .FirstOrDefaultAsync(dv => dv.MaDatVe == maDatVe);

            if (datVe == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin đơn đặt vé.";
                return RedirectToAction("Index", "LichSuDatVe");
            }

            // Tự động phát hành vé nếu đơn đã thanh toán mà chưa có vé
            bool hasNewTicket = false;
            foreach (var ct in datVe.ChiTietDatVes)
            {
                if (ct.VeDienTu == null)
                {
                    var qrCode = $"CGV-{datVe.MaDatVe}-{ct.MaChiTiet}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
                    var ve = new VeDienTu
                    {
                        MaChiTiet = ct.MaChiTiet,
                        MaQR = qrCode,
                        NgayPhatHanh = DateTime.Now,
                        TrangThai = "ChuaSuDung"
                    };
                    _context.VeDienTus.Add(ve);
                    ct.VeDienTu = ve;
                    hasNewTicket = true;
                }
            }

            if (hasNewTicket)
            {
                await _context.SaveChangesAsync();
            }

            return View(datVe);
        }

        // GET: /VeDienTu/ChiTiet/5
        public async Task<IActionResult> ChiTiet(int id)
        {
            var ve = await _context.VeDienTus
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.NguoiDung)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.Phim)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.DatVe)
                        .ThenInclude(dv => dv!.SuatChieu)
                            .ThenInclude(sc => sc!.PhongChieu)
                                .ThenInclude(pc => pc!.RapChieu)
                .Include(v => v.ChiTietDatVe)
                    .ThenInclude(ct => ct!.GheSuatChieu)
                        .ThenInclude(gsc => gsc!.GheNgoi)
                .FirstOrDefaultAsync(v => v.MaVe == id);

            if (ve == null)
            {
                TempData["Error"] = "Không tìm thấy vé điện tử.";
                return RedirectToAction("Index", "LichSuDatVe");
            }

            return View(ve);
        }
    }
}

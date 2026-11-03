using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;

namespace GiaoDienKhachHang.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly CustomerDbContext _context;

        public TrangChuController(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dangChieu = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .Where(p => p.TrangThai == "DangChieu")
                .Take(8)
                .ToListAsync();

            var sapChieu = await _context.Phims
                .Include(p => p.PhimTheLoais)
                .ThenInclude(pt => pt.TheLoai)
                .Where(p => p.TrangThai == "SapChieu")
                .Take(4)
                .ToListAsync();

            ViewBag.DangChieu = dangChieu;
            ViewBag.SapChieu = sapChieu;
            return View();
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;

namespace GiaoDienQuanTri.Controllers
{
    public class TrangChuController : ControllerBaseMvc
    {
        private readonly AdminDbContext _context;

        public TrangChuController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalUsers = await _context.NguoiDungs.CountAsync();
            ViewBag.TotalMovies = await _context.Phims.CountAsync();
            ViewBag.TotalCinemas = await _context.RapChieus.CountAsync();
            ViewBag.TotalRooms = await _context.PhongChieus.CountAsync();
            ViewBag.TotalShowtimes = await _context.SuatChieus.CountAsync();

            var recentMovies = await _context.Phims
                .OrderByDescending(p => p.MaPhim)
                .Take(5)
                .ToListAsync();

            var todayShowtimes = await _context.SuatChieus
                .Include(s => s.Phim)
                .Include(s => s.PhongChieu)
                .OrderBy(s => s.GioBatDau)
                .Take(6)
                .ToListAsync();

            ViewBag.RecentMovies = recentMovies;
            ViewBag.TodayShowtimes = todayShowtimes;

            return View();
        }
    }

    public abstract class ControllerBaseMvc : Controller
    {
    }
}


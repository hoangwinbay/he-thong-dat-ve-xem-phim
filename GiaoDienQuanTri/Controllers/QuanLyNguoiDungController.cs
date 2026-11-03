using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienQuanTri.Data;
using GiaoDienQuanTri.Models;

namespace GiaoDienQuanTri.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        private readonly AdminDbContext _context;

        public QuanLyNguoiDungController(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? vaiTro)
        {
            var query = _context.NguoiDungs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u => u.HoTen.Contains(search) || u.Email.Contains(search) || (u.SoDienThoai != null && u.SoDienThoai.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(vaiTro))
            {
                query = query.Where(u => u.VaiTro == vaiTro);
            }

            var list = await query.OrderByDescending(u => u.MaNguoiDung).ToListAsync();
            ViewBag.Search = search;
            ViewBag.VaiTro = vaiTro;
            return View(list);
        }

        public IActionResult Them()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Them(NguoiDung model)
        {
            if (await _context.NguoiDungs.AnyAsync(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email này đã được sử dụng.");
            }

            if (ModelState.IsValid)
            {
                model.NgayTao = DateTime.Now;
                _context.NguoiDungs.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm người dùng mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> CapNhat(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhat(int id, NguoiDung model)
        {
            if (id != model.MaNguoiDung) return NotFound();

            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            if (ModelState.IsValid)
            {
                user.HoTen = model.HoTen;
                user.SoDienThoai = model.SoDienThoai;
                user.VaiTro = model.VaiTro;
                user.TrangThai = model.TrangThai;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> ChiTiet(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Xoa(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user != null)
            {
                _context.NguoiDungs.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa người dùng thành công.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}


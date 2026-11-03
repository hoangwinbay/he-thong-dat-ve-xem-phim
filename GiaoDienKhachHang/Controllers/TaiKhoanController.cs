using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiaoDienKhachHang.Data;
using GiaoDienKhachHang.Models;

namespace GiaoDienKhachHang.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly CustomerDbContext _context;

        public TaiKhoanController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult DangNhap(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap(string email, string matKhau, string? returnUrl)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ Email và Mật khẩu.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var user = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.Email == email && u.MatKhau == matKhau && u.TrangThai);

            if (user == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không chính xác, hoặc tài khoản đã bị khóa.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            // Lưu session
            HttpContext.Session.SetInt32("MaNguoiDung", user.MaNguoiDung);
            HttpContext.Session.SetString("HoTen", user.HoTen);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("VaiTro", user.VaiTro);

            TempData["Success"] = $"Chào mừng bạn trở lại, {user.HoTen}!";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "TrangChu");
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(string hoTen, string email, string matKhau, string? soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập họ tên, email và mật khẩu.";
                return View();
            }

            var exists = await _context.NguoiDungs.AnyAsync(u => u.Email == email);
            if (exists)
            {
                ViewBag.Error = "Email này đã được sử dụng. Vui lòng chọn email khác.";
                return View();
            }

            var user = new NguoiDung
            {
                HoTen = hoTen.Trim(),
                Email = email.Trim(),
                MatKhau = matKhau,
                SoDienThoai = soDienThoai?.Trim(),
                VaiTro = "KhachHang",
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            _context.NguoiDungs.Add(user);
            await _context.SaveChangesAsync();

            // Tự động đăng nhập
            HttpContext.Session.SetInt32("MaNguoiDung", user.MaNguoiDung);
            HttpContext.Session.SetString("HoTen", user.HoTen);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("VaiTro", user.VaiTro);

            TempData["Success"] = "Đăng ký tài khoản thành công!";
            return RedirectToAction("Index", "TrangChu");
        }

        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đã đăng xuất tài khoản.";
            return RedirectToAction("Index", "TrangChu");
        }

        [HttpGet]
        public async Task<IActionResult> ThongTinCaNhan()
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            if (!maNguoiDung.HasValue)
            {
                return RedirectToAction("DangNhap", new { returnUrl = Url.Action("ThongTinCaNhan", "TaiKhoan") });
            }

            var user = await _context.NguoiDungs.FindAsync(maNguoiDung.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatThongTin(string hoTen, string? soDienThoai)
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            if (!maNguoiDung.HasValue) return RedirectToAction("DangNhap");

            var user = await _context.NguoiDungs.FindAsync(maNguoiDung.Value);
            if (user == null) return NotFound();

            user.HoTen = hoTen.Trim();
            user.SoDienThoai = soDienThoai?.Trim();
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("HoTen", user.HoTen);
            TempData["Success"] = "Cập nhật thông tin thành công!";
            return RedirectToAction("ThongTinCaNhan");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiMatKhau(string matKhauCu, string matKhauMoi, string xacNhanMatKhau)
        {
            var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
            if (!maNguoiDung.HasValue) return RedirectToAction("DangNhap");

            var user = await _context.NguoiDungs.FindAsync(maNguoiDung.Value);
            if (user == null) return NotFound();

            if (user.MatKhau != matKhauCu)
            {
                TempData["Error"] = "Mật khẩu hiện tại không đúng.";
                return RedirectToAction("ThongTinCaNhan");
            }

            if (string.IsNullOrWhiteSpace(matKhauMoi) || matKhauMoi.Length < 6)
            {
                TempData["Error"] = "Mật khẩu mới phải từ 6 ký tự trở lên.";
                return RedirectToAction("ThongTinCaNhan");
            }

            if (matKhauMoi != xacNhanMatKhau)
            {
                TempData["Error"] = "Xác nhận mật khẩu mới không khớp.";
                return RedirectToAction("ThongTinCaNhan");
            }

            user.MatKhau = matKhauMoi;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("ThongTinCaNhan");
        }
    }
}


using Microsoft.AspNetCore.Mvc;

namespace GiaoDienQuanTri.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult DangXuat()
        {
            TempData["SuccessMessage"] = "Đã đăng xuất khỏi hệ thống quản trị.";
            return RedirectToAction("Index", "TrangChu");
        }

        public IActionResult ThongTinCaNhan()
        {
            return View();
        }
    }
}


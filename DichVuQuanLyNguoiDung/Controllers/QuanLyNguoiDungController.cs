using Microsoft.AspNetCore.Mvc;
using DichVuQuanLyNguoiDung.DTOs;
using DichVuQuanLyNguoiDung.Services;

namespace DichVuQuanLyNguoiDung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyNguoiDungController : ControllerBase
    {
        private readonly INguoiDungService _service;

        public QuanLyNguoiDungController(INguoiDungService service)
        {
            _service = service;
        }

        /// <summary>
        /// Đăng ký tài khoản mới
        /// </summary>
        [HttpPost("dang-ky")]
        public async Task<IActionResult> DangKy([FromBody] DangKyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.DangKyAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.MaNguoiDung }, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        [HttpPost("dang-nhap")]
        public async Task<IActionResult> DangNhap([FromBody] DangNhapDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.DangNhapAsync(dto);
            if (result == null)
            {
                return Unauthorized(new { message = "Email hoặc mật khẩu không chính xác, hoặc tài khoản đã bị khóa." });
            }

            return Ok(new { message = "Đăng nhập thành công!", data = result });
        }

        /// <summary>
        /// Đăng xuất khỏi hệ thống
        /// </summary>
        [HttpPost("dang-xuat")]
        public IActionResult DangXuat()
        {
            // Xử lý hủy phiên đăng nhập ở client hoặc blacklist token
            return Ok(new { message = "Đăng xuất thành công!" });
        }

        /// <summary>
        /// Lấy danh sách toàn bộ người dùng
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// Lấy thông tin chi tiết một người dùng
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = $"Không tìm thấy người dùng có mã {id}." });
            }
            return Ok(user);
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> CapNhat(int id, [FromBody] CapNhatNguoiDungDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.CapNhatAsync(id, dto);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy người dùng có mã {id}." });
            }

            return Ok(new { message = "Cập nhật thông tin thành công!" });
        }

        /// <summary>
        /// Phân quyền người dùng (QuanTri, NhanVien, KhachHang)
        /// </summary>
        [HttpPut("{id:int}/phan-quyen")]
        public async Task<IActionResult> PhanQuyen(int id, [FromBody] PhanQuyenDTO dto)
        {
            var success = await _service.PhanQuyenAsync(id, dto);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy người dùng có mã {id}." });
            }

            return Ok(new { message = $"Đã cập nhật vai trò thành '{dto.VaiTro}'." });
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        [HttpPut("{id:int}/doi-mat-khau")]
        public async Task<IActionResult> DoiMatKhau(int id, [FromBody] DoiMatKhauDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.DoiMatKhauAsync(id, dto);
            if (!success)
            {
                return BadRequest(new { message = "Mật khẩu cũ không chính xác hoặc người dùng không tồn tại." });
            }

            return Ok(new { message = "Đổi mật khẩu thành công!" });
        }

        /// <summary>
        /// Xóa người dùng
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Xoa(int id)
        {
            var success = await _service.XoaAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy người dùng có mã {id}." });
            }

            return Ok(new { message = "Đã xóa người dùng thành công." });
        }
    }
}


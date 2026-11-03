using Microsoft.AspNetCore.Mvc;
using DichVuQuanLyPhim.DTOs;
using DichVuQuanLyPhim.Services;

namespace DichVuQuanLyPhim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyPhimController : ControllerBase
    {
        private readonly IPhimService _service;

        public QuanLyPhimController(IPhimService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lấy danh sách phim (có thể lọc theo trạng thái: DangChieu, SapChieu, NgungChieu)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? trangThai)
        {
            var phims = await _service.GetAllAsync(trangThai);
            return Ok(phims);
        }

        /// <summary>
        /// Xem thông tin chi tiết một bộ phim
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var phim = await _service.GetByIdAsync(id);
            if (phim == null)
            {
                return NotFound(new { message = $"Không tìm thấy phim có mã {id}." });
            }
            return Ok(phim);
        }

        /// <summary>
        /// Thêm phim mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ThemPhim([FromBody] ThemPhimDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _service.ThemPhimAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MaPhim }, created);
        }

        /// <summary>
        /// Cập nhật thông tin phim
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> CapNhatPhim(int id, [FromBody] CapNhatPhimDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.CapNhatPhimAsync(id, dto);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy phim có mã {id}." });
            }

            return Ok(new { message = "Cập nhật thông tin phim thành công!" });
        }

        /// <summary>
        /// Cập nhật trạng thái phim (DangChieu, SapChieu, NgungChieu)
        /// </summary>
        [HttpPatch("{id:int}/trang-thai")]
        public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] CapNhatTrangThaiDTO dto)
        {
            var success = await _service.CapNhatTrangThaiAsync(id, dto.TrangThai);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy phim có mã {id}." });
            }

            return Ok(new { message = $"Đã chuyển trạng thái phim thành '{dto.TrangThai}'." });
        }

        /// <summary>
        /// Xóa phim
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> XoaPhim(int id)
        {
            var success = await _service.XoaPhimAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy phim có mã {id}." });
            }

            return Ok(new { message = "Đã xóa phim thành công." });
        }
    }
}


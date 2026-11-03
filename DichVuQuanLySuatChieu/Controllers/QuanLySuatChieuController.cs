using Microsoft.AspNetCore.Mvc;
using DichVuQuanLySuatChieu.DTOs;
using DichVuQuanLySuatChieu.Services;

namespace DichVuQuanLySuatChieu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLySuatChieuController : ControllerBase
    {
        private readonly ISuatChieuService _service;

        public QuanLySuatChieuController(ISuatChieuService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lấy danh sách suất chiếu (có lọc theo mã phim, mã phòng, ngày chiếu, trạng thái)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] LocSuatChieuDTO filter)
        {
            var list = await _service.GetListAsync(filter);
            return Ok(list);
        }

        /// <summary>
        /// Xem thông tin chi tiết suất chiếu
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sc = await _service.GetByIdAsync(id);
            if (sc == null)
            {
                return NotFound(new { message = $"Không tìm thấy suất chiếu có mã {id}." });
            }
            return Ok(sc);
        }

        /// <summary>
        /// Tạo suất chiếu mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ThemSuatChieu([FromBody] ThemSuatChieuDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _service.ThemSuatChieuAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.MaSuatChieu }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật thông tin suất chiếu
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> CapNhatSuatChieu(int id, [FromBody] CapNhatSuatChieuDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _service.CapNhatSuatChieuAsync(id, dto);
                if (!success)
                {
                    return NotFound(new { message = $"Không tìm thấy suất chiếu có mã {id}." });
                }

                return Ok(new { message = "Cập nhật suất chiếu thành công!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật trạng thái suất chiếu (DangMoBan, HetVe, DaChieu, Huy)
        /// </summary>
        [HttpPatch("{id:int}/trang-thai")]
        public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] string trangThai)
        {
            var success = await _service.CapNhatTrangThaiAsync(id, trangThai);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy suất chiếu có mã {id}." });
            }

            return Ok(new { message = $"Đã cập nhật trạng thái suất chiếu thành '{trangThai}'." });
        }

        /// <summary>
        /// Xóa suất chiếu
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> XoaSuatChieu(int id)
        {
            var success = await _service.XoaSuatChieuAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy suất chiếu có mã {id}." });
            }

            return Ok(new { message = "Đã xóa suất chiếu thành công." });
        }
    }
}


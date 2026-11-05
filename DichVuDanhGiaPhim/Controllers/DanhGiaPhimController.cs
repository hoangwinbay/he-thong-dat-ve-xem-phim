using Microsoft.AspNetCore.Mvc;
using DichVuDanhGiaPhim.DTOs;
using DichVuDanhGiaPhim.Services;

namespace DichVuDanhGiaPhim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhGiaPhimController : ControllerBase
    {
        private readonly IDanhGiaPhimService _service;
        private readonly ILogger<DanhGiaPhimController> _logger;

        public DanhGiaPhimController(IDanhGiaPhimService service, ILogger<DanhGiaPhimController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? soSao = null, [FromQuery] int? maPhim = null)
        {
            try
            {
                var list = await _service.GetAllAsync(soSao, maPhim);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách đánh giá phim");
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);
                if (item == null)
                    return NotFound(new { message = $"Không tìm thấy đánh giá #{id}" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy đánh giá #{Id}", id);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("phim/{maPhim:int}")]
        public async Task<IActionResult> GetByPhim(int maPhim)
        {
            try
            {
                var list = await _service.GetByPhimIdAsync(maPhim);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy đánh giá của phim #{MaPhim}", maPhim);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("phim/{maPhim:int}/thong-ke")]
        public async Task<IActionResult> GetThongKePhim(int maPhim)
        {
            try
            {
                var stats = await _service.GetThongKePhimAsync(maPhim);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thống kê đánh giá của phim #{MaPhim}", maPhim);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("nguoidung/{maNguoiDung:int}")]
        public async Task<IActionResult> GetByNguoiDung(int maNguoiDung)
        {
            try
            {
                var list = await _service.GetByNguoiDungAsync(maNguoiDung);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy đánh giá của người dùng #{MaNguoiDung}", maNguoiDung);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> TaoHoacCapNhat([FromBody] TaoDanhGiaRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.TaoHoacCapNhatDanhGiaAsync(request);
                return Ok(new
                {
                    success = true,
                    message = "Đánh giá phim thành công!",
                    data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo đánh giá phim");
                return StatusCode(500, new { success = false, message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Xoa(int id)
        {
            try
            {
                var ok = await _service.XoaDanhGiaAsync(id);
                if (!ok)
                    return NotFound(new { message = $"Không tìm thấy đánh giá #{id}" });

                return Ok(new { success = true, message = "Đã xóa đánh giá thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa đánh giá #{Id}", id);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }
    }
}

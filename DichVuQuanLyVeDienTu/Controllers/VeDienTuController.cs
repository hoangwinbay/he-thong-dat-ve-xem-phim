using Microsoft.AspNetCore.Mvc;
using DichVuQuanLyVeDienTu.DTOs;
using DichVuQuanLyVeDienTu.Services;

namespace DichVuQuanLyVeDienTu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeDienTuController : ControllerBase
    {
        private readonly IVeDienTuService _service;
        private readonly ILogger<VeDienTuController> _logger;

        public VeDienTuController(IVeDienTuService service, ILogger<VeDienTuController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? trangThai = null)
        {
            try
            {
                var list = await _service.GetAllAsync(trangThai);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách vé điện tử");
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ticket = await _service.GetByIdAsync(id);
                if (ticket == null)
                    return NotFound(new { message = $"Không tìm thấy vé #{id}" });
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin vé #{Id}", id);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("datve/{maDatVe:int}")]
        public async Task<IActionResult> GetByDatVeId(int maDatVe)
        {
            try
            {
                var tickets = await _service.GetByDatVeIdAsync(maDatVe);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách vé của đơn đặt #{MaDatVe}", maDatVe);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("qr/{maQR}")]
        public async Task<IActionResult> GetByMaQR(string maQR)
        {
            try
            {
                var ticket = await _service.GetByMaQRAsync(maQR);
                if (ticket == null)
                    return NotFound(new { message = "Không tìm thấy vé với mã QR được cung cấp" });
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tìm vé theo mã QR");
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPost("phat-hanh/{maDatVe:int}")]
        public async Task<IActionResult> PhatHanhVe(int maDatVe)
        {
            try
            {
                var tickets = await _service.PhatHanhVeChoDonDatVeAsync(maDatVe);
                return Ok(new
                {
                    success = true,
                    message = $"Phát hành {tickets.Count} vé điện tử thành công!",
                    data = tickets
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi phát hành vé cho đơn #{MaDatVe}", maDatVe);
                return StatusCode(500, new { success = false, message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPost("soat-ve")]
        public async Task<IActionResult> SoatVe([FromBody] KiemTraVeRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.KiemTraVaSoatVeAsync(request.MaQR);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi soát vé");
                return StatusCode(500, new KetQuaSoatVeDTO
                {
                    HopLe = false,
                    ThongBao = "Lỗi hệ thống trong quá trình soát vé: " + ex.Message
                });
            }
        }

        [HttpPost("{id:int}/huy")]
        public async Task<IActionResult> HuyVe(int id)
        {
            try
            {
                var success = await _service.HuyVeAsync(id);
                if (!success)
                    return NotFound(new { success = false, message = "Không tìm thấy vé cần hủy" });

                return Ok(new { success = true, message = "Đã hủy vé thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi hủy vé #{Id}", id);
                return StatusCode(500, new { success = false, message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }
    }
}

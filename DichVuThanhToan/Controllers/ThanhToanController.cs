using Microsoft.AspNetCore.Mvc;
using DichVuThanhToan.DTOs;
using DichVuThanhToan.Services;

namespace DichVuThanhToan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanService _service;
        private readonly ILogger<ThanhToanController> _logger;

        public ThanhToanController(IThanhToanService service, ILogger<ThanhToanController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Tạo và xác nhận giao dịch thanh toán cho đơn đặt vé
        /// </summary>
        [HttpPost("tao-giao-dich")]
        public async Task<IActionResult> TaoGiaoDich([FromBody] TaoGiaoDichThanhToanDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.TaoVaXacNhanGiaoDichAsync(dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xử lý giao dịch thanh toán");
                return StatusCode(500, new { message = "Lỗi hệ thống khi xử lý thanh toán" });
            }
        }

        /// <summary>
        /// Lấy chi tiết giao dịch thanh toán theo ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tt = await _service.GetByIdAsync(id);
            if (tt == null)
            {
                return NotFound(new { message = $"Không tìm thấy giao dịch thanh toán #{id}" });
            }
            return Ok(tt);
        }

        /// <summary>
        /// Lấy giao dịch thanh toán theo mã đơn đặt vé
        /// </summary>
        [HttpGet("datve/{maDatVe:int}")]
        public async Task<IActionResult> GetByDatVeId(int maDatVe)
        {
            var tt = await _service.GetByDatVeIdAsync(maDatVe);
            if (tt == null)
            {
                return NotFound(new { message = $"Đơn đặt vé #{maDatVe} chưa có thông tin thanh toán" });
            }
            return Ok(tt);
        }

        /// <summary>
        /// Lấy tất cả giao dịch thanh toán (hỗ trợ lọc theo trạng thái, phương thức)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? trangThai = null, [FromQuery] string? phuongThuc = null)
        {
            var list = await _service.GetAllAsync(trangThai, phuongThuc);
            return Ok(list);
        }

        /// <summary>
        /// Cập nhật trạng thái giao dịch thanh toán
        /// </summary>
        [HttpPut("{id:int}/trang-thai")]
        public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] CapNhatTrangThaiThanhToanDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.CapNhatTrangThaiAsync(id, dto.TrangThai, dto.MaGiaoDich);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy giao dịch #{id}" });
            }
            return Ok(new { message = $"Đã cập nhật trạng thái giao dịch #{id} thành '{dto.TrangThai}' thành công." });
        }

        /// <summary>
        /// Hoàn tiền giao dịch thanh toán và hủy vé
        /// </summary>
        [HttpPost("{id:int}/hoan-tien")]
        public async Task<IActionResult> HoanTien(int id)
        {
            var success = await _service.HoanTienAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy giao dịch #{id}" });
            }
            return Ok(new { message = $"Đã thực hiện hoàn tiền giao dịch #{id} thành công và giải phóng vé." });
        }
    }
}

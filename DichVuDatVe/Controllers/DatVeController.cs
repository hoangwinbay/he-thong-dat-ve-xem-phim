using Microsoft.AspNetCore.Mvc;
using DichVuDatVe.DTOs;
using DichVuDatVe.Services;

namespace DichVuDatVe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatVeController : ControllerBase
    {
        private readonly IDatVeService _service;
        private readonly ILogger<DatVeController> _logger;

        public DatVeController(IDatVeService service, ILogger<DatVeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Tạo đơn đặt vé mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> TaoDonDatVe([FromBody] TaoDonDatVeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var donDatVe = await _service.TaoDonDatVeAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = donDatVe.MaDatVe }, donDatVe);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo đơn đặt vé");
                return StatusCode(500, new { message = "Lỗi hệ thống khi tạo đơn đặt vé" });
            }
        }

        /// <summary>
        /// Lấy tất cả đơn đặt vé (hỗ trợ lọc theo trạng thái, suất chiếu)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? trangThai = null, [FromQuery] int? maSuatChieu = null)
        {
            var list = await _service.GetAllAsync(trangThai, maSuatChieu);
            return Ok(list);
        }

        /// <summary>
        /// Lấy chi tiết đơn đặt vé theo ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donDatVe = await _service.GetByIdAsync(id);
            if (donDatVe == null)
            {
                return NotFound(new { message = $"Không tìm thấy đơn đặt vé với mã {id}" });
            }
            return Ok(donDatVe);
        }

        /// <summary>
        /// Lấy lịch sử đặt vé của một khách hàng
        /// </summary>
        [HttpGet("nguoidung/{maNguoiDung:int}")]
        public async Task<IActionResult> GetByNguoiDung(int maNguoiDung)
        {
            var list = await _service.GetByNguoiDungAsync(maNguoiDung);
            return Ok(list);
        }

        /// <summary>
        /// Cập nhật trạng thái đơn đặt vé
        /// </summary>
        [HttpPut("{id:int}/trang-thai")]
        public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] CapNhatTrangThaiDatVeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.CapNhatTrangThaiAsync(id, dto.TrangThai);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy đơn đặt vé với mã {id}" });
            }
            return Ok(new { message = $"Đã cập nhật trạng thái đơn vé {id} thành '{dto.TrangThai}' thành công." });
        }

        /// <summary>
        /// Hủy đơn đặt vé
        /// </summary>
        [HttpPost("{id:int}/huy")]
        public async Task<IActionResult> HuyDonDatVe(int id)
        {
            var success = await _service.HuyDonDatVeAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy đơn đặt vé với mã {id}" });
            }
            return Ok(new { message = $"Đã hủy đơn đặt vé {id} thành công và giải phóng ghế." });
        }
    }
}

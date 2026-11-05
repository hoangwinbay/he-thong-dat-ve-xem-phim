using Microsoft.AspNetCore.Mvc;
using DichVuThongBao.DTOs;
using DichVuThongBao.Services;

namespace DichVuThongBao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThongBaoController : ControllerBase
    {
        private readonly IThongBaoService _service;
        private readonly ILogger<ThongBaoController> _logger;

        public ThongBaoController(IThongBaoService service, ILogger<ThongBaoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? daDoc = null)
        {
            try
            {
                var list = await _service.GetAllAsync(daDoc);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách thông báo");
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
                    return NotFound(new { message = $"Không tìm thấy thông báo #{id}" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông báo #{Id}", id);
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
                _logger.LogError(ex, "Lỗi khi lấy thông báo của người dùng #{MaNguoiDung}", maNguoiDung);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpGet("chua-doc/{maNguoiDung:int}")]
        public async Task<IActionResult> DemSoChuaDoc(int maNguoiDung)
        {
            try
            {
                var count = await _service.DemSoChuaDocAsync(maNguoiDung);
                return Ok(new { maNguoiDung, soChuaDoc = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đếm thông báo chưa đọc");
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuiThongBao([FromBody] GuiThongBaoRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.GuiThongBaoAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = created.MaThongBao }, created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi thông báo");
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPost("gui-tat-ca")]
        public async Task<IActionResult> GuiTatCa([FromBody] GuiThongBaoTatCaRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var count = await _service.GuiThongBaoChoTatCaAsync(request.TieuDe, request.NoiDung);
                return Ok(new
                {
                    success = true,
                    message = $"Đã phát sóng thông báo tới {count} khách hàng thành công!",
                    soNguoiNhan = count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi thông báo đồng loạt");
                return StatusCode(500, new { success = false, message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPut("{id:int}/doc")]
        public async Task<IActionResult> DanhDauDaDoc(int id)
        {
            try
            {
                var ok = await _service.DanhDauDaDocAsync(id);
                if (!ok)
                    return NotFound(new { message = $"Không tìm thấy thông báo #{id}" });

                return Ok(new { success = true, message = "Đã đánh dấu thông báo là đã đọc" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật trạng thái đã đọc cho thông báo #{Id}", id);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpPut("nguoidung/{maNguoiDung:int}/doc-tat-ca")]
        public async Task<IActionResult> DanhDauTatCaDaDoc(int maNguoiDung)
        {
            try
            {
                var ok = await _service.DanhDauTatCaDaDocAsync(maNguoiDung);
                return Ok(new { success = ok, message = "Đã đánh dấu tất cả thông báo là đã đọc" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đánh dấu tất cả đã đọc cho #{MaNguoiDung}", maNguoiDung);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Xoa(int id)
        {
            try
            {
                var ok = await _service.XoaThongBaoAsync(id);
                if (!ok)
                    return NotFound(new { message = $"Không tìm thấy thông báo #{id}" });

                return Ok(new { success = true, message = "Đã xóa thông báo thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa thông báo #{Id}", id);
                return StatusCode(500, new { message = "Lỗi máy chủ nội bộ: " + ex.Message });
            }
        }
    }
}

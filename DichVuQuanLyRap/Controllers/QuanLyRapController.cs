using Microsoft.AspNetCore.Mvc;
using DichVuQuanLyRap.DTOs;
using DichVuQuanLyRap.Services;

namespace DichVuQuanLyRap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyRapController : ControllerBase
    {
        private readonly IRapService _service;

        public QuanLyRapController(IRapService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lấy danh sách tất cả các rạp chiếu phim
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllRap()
        {
            var raps = await _service.GetAllRapAsync();
            return Ok(raps);
        }

        /// <summary>
        /// Lấy chi tiết thông tin một rạp
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRapById(int id)
        {
            var rap = await _service.GetRapByIdAsync(id);
            if (rap == null)
            {
                return NotFound(new { message = $"Không tìm thấy rạp có mã {id}." });
            }
            return Ok(rap);
        }

        /// <summary>
        /// Thêm cụm rạp mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ThemRap([FromBody] ThemRapDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _service.ThemRapAsync(dto);
            return CreatedAtAction(nameof(GetRapById), new { id = created.MaRap }, created);
        }

        /// <summary>
        /// Cập nhật thông tin cụm rạp
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> CapNhatRap(int id, [FromBody] CapNhatRapDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.CapNhatRapAsync(id, dto);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy rạp có mã {id}." });
            }

            return Ok(new { message = "Cập nhật thông tin rạp thành công!" });
        }

        /// <summary>
        /// Xóa rạp chiếu phim
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> XoaRap(int id)
        {
            var success = await _service.XoaRapAsync(id);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy rạp có mã {id}." });
            }

            return Ok(new { message = "Đã xóa rạp chiếu thành công." });
        }

        // ================= Quản lý phòng chiếu =================

        /// <summary>
        /// Lấy danh sách phòng chiếu thuộc về một rạp
        /// </summary>
        [HttpGet("{maRap:int}/phong-chieu")]
        public async Task<IActionResult> GetPhongChieuByRap(int maRap)
        {
            var phongs = await _service.GetPhongChieuByRapAsync(maRap);
            return Ok(phongs);
        }

        /// <summary>
        /// Lấy chi tiết phòng chiếu theo mã phòng
        /// </summary>
        [HttpGet("phong-chieu/{maPhong:int}")]
        public async Task<IActionResult> GetPhongChieuById(int maPhong)
        {
            var phong = await _service.GetPhongChieuByIdAsync(maPhong);
            if (phong == null)
            {
                return NotFound(new { message = $"Không tìm thấy phòng chiếu có mã {maPhong}." });
            }
            return Ok(phong);
        }

        /// <summary>
        /// Thêm phòng chiếu mới vào rạp
        /// </summary>
        [HttpPost("{maRap:int}/phong-chieu")]
        public async Task<IActionResult> ThemPhongChieu(int maRap, [FromBody] ThemPhongChieuDTO dto)
        {
            dto.MaRap = maRap;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _service.ThemPhongChieuAsync(dto);
            return CreatedAtAction(nameof(GetPhongChieuById), new { maPhong = created.MaPhong }, created);
        }

        /// <summary>
        /// Cập nhật thông tin phòng chiếu
        /// </summary>
        [HttpPut("phong-chieu/{maPhong:int}")]
        public async Task<IActionResult> CapNhatPhongChieu(int maPhong, [FromBody] CapNhatPhongChieuDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.CapNhatPhongChieuAsync(maPhong, dto);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy phòng chiếu có mã {maPhong}." });
            }

            return Ok(new { message = "Cập nhật phòng chiếu thành công!" });
        }

        /// <summary>
        /// Xóa phòng chiếu
        /// </summary>
        [HttpDelete("phong-chieu/{maPhong:int}")]
        public async Task<IActionResult> XoaPhongChieu(int maPhong)
        {
            var success = await _service.XoaPhongChieuAsync(maPhong);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy phòng chiếu có mã {maPhong}." });
            }

            return Ok(new { message = "Đã xóa phòng chiếu thành công." });
        }
    }
}


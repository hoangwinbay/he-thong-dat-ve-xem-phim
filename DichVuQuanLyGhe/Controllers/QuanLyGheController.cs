using Microsoft.AspNetCore.Mvc;
using DichVuQuanLyGhe.DTOs;
using DichVuQuanLyGhe.Services;

namespace DichVuQuanLyGhe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuanLyGheController : ControllerBase
    {
        private readonly IGheService _service;

        public QuanLyGheController(IGheService service)
        {
            _service = service;
        }

        // ================= Quản lý ghế vật lý trong phòng =================

        /// <summary>
        /// Lấy sơ đồ ghế vật lý của một phòng chiếu
        /// </summary>
        [HttpGet("phong/{maPhong:int}")]
        public async Task<IActionResult> GetGheByPhong(int maPhong)
        {
            var ghes = await _service.GetGheByPhongAsync(maPhong);
            return Ok(ghes);
        }

        /// <summary>
        /// Thêm một ghế đơn lẻ vào phòng chiếu
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ThemGhe([FromBody] ThemGheDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _service.ThemGheAsync(dto);
            return Ok(created);
        }

        /// <summary>
        /// Tự động sinh sơ đồ ghế mặc định (hàng A-F, mỗi hàng 8 ghế)
        /// </summary>
        [HttpPost("phong/{maPhong:int}/tao-so-do-mac-dinh")]
        public async Task<IActionResult> TaoSoDoGheMacDinh(int maPhong, [FromQuery] int soHang = 6, [FromQuery] int soGheMoiHang = 8)
        {
            var count = await _service.TaoSoDoGheMacDinhAsync(maPhong, soHang, soGheMoiHang);
            return Ok(new { message = $"Đã tạo thành công {count} ghế cho phòng chiếu {maPhong}." });
        }

        /// <summary>
        /// Cập nhật loại ghế / trạng thái ghế vật lý
        /// </summary>
        [HttpPut("{maGhe:int}")]
        public async Task<IActionResult> CapNhatGhe(int maGhe, [FromBody] CapNhatGheDTO dto)
        {
            var success = await _service.CapNhatGheAsync(maGhe, dto);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy ghế có mã {maGhe}." });
            }

            return Ok(new { message = "Cập nhật thông tin ghế thành công!" });
        }

        /// <summary>
        /// Xóa ghế vật lý
        /// </summary>
        [HttpDelete("{maGhe:int}")]
        public async Task<IActionResult> XoaGhe(int maGhe)
        {
            var success = await _service.XoaGheAsync(maGhe);
            if (!success)
            {
                return NotFound(new { message = $"Không tìm thấy ghế có mã {maGhe}." });
            }

            return Ok(new { message = "Đã xóa ghế thành công." });
        }

        // ================= Quản lý ghế theo suất chiếu =================

        /// <summary>
        /// Lấy sơ đồ ghế và trạng thái đặt chỗ (Trống / Đang giữ / Đã đặt) của một suất chiếu
        /// </summary>
        [HttpGet("suat-chieu/{maSuatChieu:int}")]
        public async Task<IActionResult> GetSoDoGheSuatChieu(int maSuatChieu)
        {
            var soDo = await _service.GetSoDoGheSuatChieuAsync(maSuatChieu);
            return Ok(soDo);
        }

        /// <summary>
        /// Khởi tạo danh sách ghế cho một suất chiếu mới từ danh sách ghế phòng
        /// </summary>
        [HttpPost("suat-chieu/khoi-tao")]
        public async Task<IActionResult> KhoiTaoGheSuatChieu([FromBody] KhoiTaoGheSuatChieuDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var count = await _service.KhoiTaoGheSuatChieuAsync(dto);
            return Ok(new { message = $"Đã khởi tạo {count} ghế cho suất chiếu {dto.MaSuatChieu}." });
        }

        /// <summary>
        /// Giữ ghế tạm thời (chuyển sang trạng thái DangGiu kèm thời gian hết hạn)
        /// </summary>
        [HttpPost("giu-ghe")]
        public async Task<IActionResult> GiuGhe([FromBody] GiuGheRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _service.GiuGheAsync(dto);
            if (!success)
            {
                return Conflict(new { message = "Một hoặc nhiều ghế bạn chọn đã bị người khác giữ hoặc đặt trước." });
            }

            return Ok(new { message = $"Giữ ghế thành công trong {dto.SoPhutGiu} phút." });
        }

        /// <summary>
        /// Giải phóng ghế giữ (khi hủy hoặc thoát màn hình chọn ghế)
        /// </summary>
        [HttpPost("giai-phong-ghe")]
        public async Task<IActionResult> GiaiPhongGhe([FromBody] GiaiPhongGheRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.GiaiPhongGheAsync(dto);
            return Ok(new { message = "Đã giải phóng ghế thành công." });
        }

        /// <summary>
        /// Đánh dấu ghế đã đặt (chuyển sang trạng thái DaDat sau khi thanh toán)
        /// </summary>
        [HttpPost("dat-ghe")]
        public async Task<IActionResult> DatGhe([FromBody] List<int> danhSachMaGheSuatChieu)
        {
            var success = await _service.DatGheAsync(danhSachMaGheSuatChieu);
            if (!success)
            {
                return BadRequest(new { message = "Danh sách ghế không hợp lệ." });
            }

            return Ok(new { message = "Đã cập nhật trạng thái ghế thành 'DaDat'." });
        }
    }
}


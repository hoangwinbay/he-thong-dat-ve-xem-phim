using Microsoft.AspNetCore.Mvc;
using CongKetNoiAPI.Services;

namespace CongKetNoiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly IGatewayAggregatorService _aggregatorService;

        public AggregationController(IGatewayAggregatorService aggregatorService)
        {
            _aggregatorService = aggregatorService;
        }

        [HttpGet("phim/{id}")]
        public async Task<IActionResult> GetPhimWithSuatChieu(int id)
        {
            var data = await _aggregatorService.GetPhimWithSuatChieuAsync(id);
            if (data.Phim == null)
            {
                return NotFound(new { message = $"Không tìm thấy thông tin phim với mã {id}" });
            }
            return Ok(data);
        }

        [HttpGet("suatchieu/{id}/ghe")]
        public async Task<IActionResult> GetSuatChieuWithGhe(int id)
        {
            var data = await _aggregatorService.GetSuatChieuWithGheAsync(id);
            if (data.SuatChieu == null)
            {
                return NotFound(new { message = $"Không tìm thấy thông tin suất chiếu với mã {id}" });
            }
            return Ok(data);
        }

        [HttpGet("rap/{id}")]
        public async Task<IActionResult> GetRapWithPhongChieu(int id)
        {
            var data = await _aggregatorService.GetRapWithPhongChieuAsync(id);
            return Ok(data);
        }
    }
}


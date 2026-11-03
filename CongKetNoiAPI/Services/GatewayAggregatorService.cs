using System.Text.Json;
using CongKetNoiAPI.Models;

namespace CongKetNoiAPI.Services
{
    public interface IGatewayAggregatorService
    {
        Task<AggregatedPhimSuatChieuDto> GetPhimWithSuatChieuAsync(int maPhim);
        Task<AggregatedSuatChieuGheDto> GetSuatChieuWithGheAsync(int maSuatChieu);
        Task<object> GetRapWithPhongChieuAsync(int maRap);
    }

    public class GatewayAggregatorService : IGatewayAggregatorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IGatewayService _gatewayService;
        private readonly ILogger<GatewayAggregatorService> _logger;

        public GatewayAggregatorService(IHttpClientFactory httpClientFactory, IGatewayService gatewayService, ILogger<GatewayAggregatorService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _gatewayService = gatewayService;
            _logger = logger;
        }

        public async Task<AggregatedPhimSuatChieuDto> GetPhimWithSuatChieuAsync(int maPhim)
        {
            var services = _gatewayService.GetRegisteredServices();
            var phimService = services.FirstOrDefault(s => s.Id == "DichVuQuanLyPhim");
            var suatChieuService = services.FirstOrDefault(s => s.Id == "DichVuQuanLySuatChieu");

            var client = _httpClientFactory.CreateClient("GatewayForwardClient");

            var phimTask = FetchJsonAsync(client, $"{phimService?.BaseUrl}/api/QuanLyPhim/{maPhim}");
            var suatChieuTask = FetchJsonAsync(client, $"{suatChieuService?.BaseUrl}/api/QuanLySuatChieu?maPhim={maPhim}");

            await Task.WhenAll(phimTask, suatChieuTask);

            return new AggregatedPhimSuatChieuDto
            {
                Phim = await phimTask,
                SuatChieus = await suatChieuTask,
                ThoiGianTongHop = DateTime.Now
            };
        }

        public async Task<AggregatedSuatChieuGheDto> GetSuatChieuWithGheAsync(int maSuatChieu)
        {
            var services = _gatewayService.GetRegisteredServices();
            var suatChieuService = services.FirstOrDefault(s => s.Id == "DichVuQuanLySuatChieu");
            var gheService = services.FirstOrDefault(s => s.Id == "DichVuQuanLyGhe");

            var client = _httpClientFactory.CreateClient("GatewayForwardClient");

            var suatChieuTask = FetchJsonAsync(client, $"{suatChieuService?.BaseUrl}/api/QuanLySuatChieu/{maSuatChieu}");
            var gheTask = FetchJsonAsync(client, $"{gheService?.BaseUrl}/api/QuanLyGhe/suat-chieu/{maSuatChieu}");

            await Task.WhenAll(suatChieuTask, gheTask);

            return new AggregatedSuatChieuGheDto
            {
                SuatChieu = await suatChieuTask,
                SoDoGhe = await gheTask,
                ThoiGianTongHop = DateTime.Now
            };
        }

        public async Task<object> GetRapWithPhongChieuAsync(int maRap)
        {
            var services = _gatewayService.GetRegisteredServices();
            var rapService = services.FirstOrDefault(s => s.Id == "DichVuQuanLyRap");

            var client = _httpClientFactory.CreateClient("GatewayForwardClient");

            var rapTask = FetchJsonAsync(client, $"{rapService?.BaseUrl}/api/QuanLyRap/{maRap}");
            var phongChieuTask = FetchJsonAsync(client, $"{rapService?.BaseUrl}/api/QuanLyRap/{maRap}/phong-chieu");

            await Task.WhenAll(rapTask, phongChieuTask);

            return new
            {
                Rap = await rapTask,
                PhongChieus = await phongChieuTask,
                ThoiGianTongHop = DateTime.Now
            };
        }

        private async Task<object?> FetchJsonAsync(HttpClient client, string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            try
            {
                using var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<object>(stream);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Không thể lấy dữ liệu từ URL {Url}: {Message}", url, ex.Message);
                return null;
            }
        }
    }
}


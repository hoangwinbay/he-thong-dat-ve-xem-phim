using System.Diagnostics;
using CongKetNoiAPI.Models;

namespace CongKetNoiAPI.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly List<ServiceRouteConfig> _services = new();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GatewayService> _logger;

        public GatewayService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<GatewayService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;

            var section = configuration.GetSection("Microservices");
            foreach (var child in section.GetChildren())
            {
                var service = child.Get<ServiceRouteConfig>();
                if (service != null && !string.IsNullOrWhiteSpace(service.Prefix))
                {
                    _services.Add(service);
                }
            }
        }

        public List<ServiceRouteConfig> GetRegisteredServices()
        {
            return _services;
        }

        public ServiceRouteConfig? MatchRoute(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return null;

            var normalizedPath = path.ToLowerInvariant();
            return _services.FirstOrDefault(s =>
                normalizedPath.Equals(s.Prefix.ToLowerInvariant(), StringComparison.OrdinalIgnoreCase) ||
                normalizedPath.StartsWith(s.Prefix.ToLowerInvariant() + "/", StringComparison.OrdinalIgnoreCase));
        }

        public async Task<GatewayHealthResponse> CheckAllServicesHealthAsync()
        {
            var tasks = _services.Select(s => CheckServiceHealthAsync(s.Id));
            var results = await Task.WhenAll(tasks);

            var list = results.ToList();
            var healthyCount = list.Count(r => r.IsHealthy);

            return new GatewayHealthResponse
            {
                GatewayName = "CINEBOOKING API Gateway",
                Version = "1.0.0",
                Status = healthyCount == list.Count ? "Tất cả dịch vụ hoạt động tốt" : $"{healthyCount}/{list.Count} dịch vụ sẵn sàng",
                Timestamp = DateTime.Now,
                TotalServices = list.Count,
                HealthyServices = healthyCount,
                Services = list
            };
        }

        public async Task<ServiceHealthStatus> CheckServiceHealthAsync(string serviceId)
        {
            var service = _services.FirstOrDefault(s => s.Id.Equals(serviceId, StringComparison.OrdinalIgnoreCase));
            if (service == null)
            {
                return new ServiceHealthStatus
                {
                    Id = serviceId,
                    Name = "Không xác định",
                    IsHealthy = false,
                    ErrorMessage = "Không tìm thấy cấu hình dịch vụ trong Gateway."
                };
            }

            var status = new ServiceHealthStatus
            {
                Id = service.Id,
                Name = service.Name,
                BaseUrl = service.BaseUrl,
                Prefix = service.Prefix,
                LastChecked = DateTime.Now
            };

            var client = _httpClientFactory.CreateClient("GatewayPingClient");
            client.Timeout = TimeSpan.FromSeconds(3);

            var sw = Stopwatch.StartNew();
            try
            {
                var targetUrl = $"{service.BaseUrl.TrimEnd('/')}/{service.HealthCheckPath.TrimStart('/')}";
                using var response = await client.GetAsync(targetUrl);
                sw.Stop();

                status.ResponseTimeMs = sw.ElapsedMilliseconds;
                status.StatusCode = (int)response.StatusCode;
                status.IsHealthy = response.IsSuccessStatusCode;
                if (!response.IsSuccessStatusCode)
                {
                    status.ErrorMessage = $"HTTP {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                status.ResponseTimeMs = sw.ElapsedMilliseconds;
                status.IsHealthy = false;
                status.ErrorMessage = ex.Message;
                _logger.LogWarning("Kiểm tra sức khỏe dịch vụ {ServiceName} ({BaseUrl}) thất bại: {Message}", service.Name, service.BaseUrl, ex.Message);
            }

            return status;
        }

        public List<RouteInfoDto> GetRouteInfoList()
        {
            return _services.Select(s => new RouteInfoDto
            {
                ServiceName = s.Name,
                RoutePrefix = s.Prefix,
                TargetBaseUrl = s.BaseUrl,
                TargetPrefix = s.TargetPrefix,
                Description = s.Description,
                SampleClientUrl = $"http://localhost:5000{s.Prefix}",
                SampleForwardedUrl = $"{s.BaseUrl}{s.TargetPrefix}"
            }).ToList();
        }
    }
}


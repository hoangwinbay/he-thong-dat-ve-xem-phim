namespace CongKetNoiAPI.Models
{
    public class ServiceRouteConfig
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public string TargetPrefix { get; set; } = string.Empty;
        public string HealthCheckPath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class ServiceHealthStatus
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public bool IsHealthy { get; set; }
        public long ResponseTimeMs { get; set; }
        public int? StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime LastChecked { get; set; } = DateTime.Now;
    }

    public class GatewayHealthResponse
    {
        public string GatewayName { get; set; } = "CINEBOOKING API Gateway";
        public string Version { get; set; } = "1.0.0";
        public string Status { get; set; } = "Healthy";
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int TotalServices { get; set; }
        public int HealthyServices { get; set; }
        public List<ServiceHealthStatus> Services { get; set; } = new();
    }

    public class RouteInfoDto
    {
        public string RoutePrefix { get; set; } = string.Empty;
        public string TargetBaseUrl { get; set; } = string.Empty;
        public string TargetPrefix { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SampleClientUrl { get; set; } = string.Empty;
        public string SampleForwardedUrl { get; set; } = string.Empty;
    }

    public class AggregatedPhimSuatChieuDto
    {
        public object? Phim { get; set; }
        public object? SuatChieus { get; set; }
        public DateTime ThoiGianTongHop { get; set; } = DateTime.Now;
    }

    public class AggregatedSuatChieuGheDto
    {
        public object? SuatChieu { get; set; }
        public object? SoDoGhe { get; set; }
        public DateTime ThoiGianTongHop { get; set; } = DateTime.Now;
    }
}


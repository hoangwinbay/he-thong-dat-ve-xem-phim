using CongKetNoiAPI.Models;

namespace CongKetNoiAPI.Services
{
    public interface IGatewayService
    {
        List<ServiceRouteConfig> GetRegisteredServices();
        ServiceRouteConfig? MatchRoute(string path);
        Task<GatewayHealthResponse> CheckAllServicesHealthAsync();
        Task<ServiceHealthStatus> CheckServiceHealthAsync(string serviceId);
        List<RouteInfoDto> GetRouteInfoList();
    }
}


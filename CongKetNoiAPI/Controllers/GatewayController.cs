using Microsoft.AspNetCore.Mvc;
using CongKetNoiAPI.Services;

namespace CongKetNoiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;

        public GatewayController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpGet("health")]
        public async Task<IActionResult> GetHealth()
        {
            var health = await _gatewayService.CheckAllServicesHealthAsync();
            return Ok(health);
        }

        [HttpGet("services")]
        public IActionResult GetServices()
        {
            var services = _gatewayService.GetRegisteredServices();
            return Ok(services);
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes()
        {
            var routes = _gatewayService.GetRouteInfoList();
            return Ok(routes);
        }

        [HttpGet("ping/{serviceId}")]
        public async Task<IActionResult> PingService(string serviceId)
        {
            var result = await _gatewayService.CheckServiceHealthAsync(serviceId);
            return Ok(result);
        }
    }
}


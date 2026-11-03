using Microsoft.AspNetCore.Mvc;
using CongKetNoiAPI.Services;

namespace CongKetNoiAPI.Controllers
{
    [ApiController]
    public class ReverseProxyController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReverseProxyController> _logger;

        public ReverseProxyController(IGatewayService gatewayService, IHttpClientFactory httpClientFactory, ILogger<ReverseProxyController> logger)
        {
            _gatewayService = gatewayService;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [Route("api/{serviceKey}/{**catchAll}")]
        public async Task Proxy(string serviceKey, string? catchAll)
        {
            // Bỏ qua nếu là các route đặc biệt của Gateway
            if (serviceKey.Equals("gateway", StringComparison.OrdinalIgnoreCase) ||
                serviceKey.Equals("aggregation", StringComparison.OrdinalIgnoreCase))
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            var requestPath = $"/api/{serviceKey}";
            var service = _gatewayService.MatchRoute(requestPath);

            if (service == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                Response.ContentType = "application/json";
                await Response.WriteAsJsonAsync(new
                {
                    error = "RouteNotFound",
                    message = $"Không tìm thấy dịch vụ tương ứng với tiền tố '/api/{serviceKey}' trên Cổng kết nối API (CongKetNoiAPI)."
                });
                return;
            }

            // Xây dựng URL đích phía Microservice
            var targetUrl = $"{service.BaseUrl.TrimEnd('/')}{service.TargetPrefix}";
            if (!string.IsNullOrEmpty(catchAll))
            {
                targetUrl += $"/{catchAll.TrimStart('/')}";
            }
            if (Request.QueryString.HasValue)
            {
                targetUrl += Request.QueryString.Value;
            }

            var client = _httpClientFactory.CreateClient("GatewayForwardClient");

            using var forwardRequest = new HttpRequestMessage(new HttpMethod(Request.Method), targetUrl);

            // Copy Headers (bỏ qua các hop-by-hop header)
            foreach (var header in Request.Headers)
            {
                if (header.Key.Equals("Host", StringComparison.OrdinalIgnoreCase) ||
                    header.Key.Equals("Connection", StringComparison.OrdinalIgnoreCase) ||
                    header.Key.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (!forwardRequest.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                {
                    forwardRequest.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }

            // Copy Request Body nếu là POST, PUT, PATCH
            if (HttpMethods.IsPost(Request.Method) || HttpMethods.IsPut(Request.Method) || HttpMethods.IsPatch(Request.Method))
            {
                forwardRequest.Content = new StreamContent(Request.Body);
                if (Request.ContentType != null)
                {
                    forwardRequest.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(Request.ContentType);
                }
            }

            try
            {
                using var response = await client.SendAsync(forwardRequest, HttpCompletionOption.ResponseHeadersRead, HttpContext.RequestAborted);

                Response.StatusCode = (int)response.StatusCode;

                // Copy Response Headers
                foreach (var header in response.Headers)
                {
                    Response.Headers[header.Key] = header.Value.ToArray();
                }
                foreach (var header in response.Content.Headers)
                {
                    Response.Headers[header.Key] = header.Value.ToArray();
                }

                Response.Headers.Remove("transfer-encoding");

                // Stream nội dung phản hồi về client
                await response.Content.CopyToAsync(Response.Body, HttpContext.RequestAborted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi chuyển tiếp request từ Gateway tới {TargetUrl}: {Message}", targetUrl, ex.Message);
                Response.StatusCode = StatusCodes.Status502BadGateway;
                Response.ContentType = "application/json";
                await Response.WriteAsJsonAsync(new
                {
                    error = "BadGateway",
                    message = $"Không thể kết nối tới microservice '{service.Name}' ({service.BaseUrl}). Vui lòng kiểm tra dịch vụ đã được khởi động chưa.",
                    detail = ex.Message,
                    targetUrl = targetUrl
                });
            }
        }
    }
}


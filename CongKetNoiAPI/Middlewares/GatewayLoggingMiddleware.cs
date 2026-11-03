using System.Diagnostics;

namespace CongKetNoiAPI.Middlewares
{
    public class GatewayLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GatewayLoggingMiddleware> _logger;

        public GatewayLoggingMiddleware(RequestDelegate next, ILogger<GatewayLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var path = context.Request.Path;
            var method = context.Request.Method;

            try
            {
                await _next(context);
                sw.Stop();

                // Chỉ ghi log cho các request gọi API
                if (path.StartsWithSegments("/api"))
                {
                    _logger.LogInformation("[Gateway] {Method} {Path} -> Phản hồi {StatusCode} ({Duration}ms)",
                        method, path, context.Response.StatusCode, sw.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "[Gateway Error] {Method} {Path} thất bại sau {Duration}ms: {Message}",
                    method, path, sw.ElapsedMilliseconds, ex.Message);
                throw;
            }
        }
    }
}


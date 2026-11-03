using CongKetNoiAPI.Middlewares;
using CongKetNoiAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Cấu hình HttpClients phục vụ Gateway Ping & Chuyển tiếp Request
builder.Services.AddHttpClient("GatewayPingClient", client =>
{
    client.Timeout = TimeSpan.FromSeconds(3);
});

builder.Services.AddHttpClient("GatewayForwardClient", client =>
{
    client.Timeout = TimeSpan.FromSeconds(20);
});

// Đăng ký dịch vụ Gateway và Gom nhóm API (Aggregator)
builder.Services.AddSingleton<IGatewayService, GatewayService>();
builder.Services.AddScoped<IGatewayAggregatorService, GatewayAggregatorService>();

// Cấu hình CORS mở rộng cho các ứng dụng Front-End và Quản trị
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Cấu hình pipeline HTTP
app.UseCors("AllowAllOrigins");

// Middleware ghi log các lượt gọi API qua Gateway
app.UseMiddleware<GatewayLoggingMiddleware>();

// Phục vụ giao diện điều khiển Console trực quan tại wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();


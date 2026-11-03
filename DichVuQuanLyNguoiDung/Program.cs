using Microsoft.EntityFrameworkCore;
using DichVuQuanLyNguoiDung.Data;
using DichVuQuanLyNguoiDung.Repositories;
using DichVuQuanLyNguoiDung.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext kết nối CSDL_DatVeXemPhim
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NguoiDungDbContext>(options =>
    options.UseSqlServer(connectionString));

// Đăng ký Dependency Injection cho Repository và Service
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();

// CORS cho phép Frontend gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

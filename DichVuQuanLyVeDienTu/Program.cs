using Microsoft.EntityFrameworkCore;
using DichVuQuanLyVeDienTu.Data;
using DichVuQuanLyVeDienTu.Repositories;
using DichVuQuanLyVeDienTu.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VeDienTuDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IVeDienTuRepository, VeDienTuRepository>();
builder.Services.AddScoped<IVeDienTuService, VeDienTuService>();

// CORS
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

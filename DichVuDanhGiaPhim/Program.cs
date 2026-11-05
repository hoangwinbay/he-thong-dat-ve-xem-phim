using Microsoft.EntityFrameworkCore;
using DichVuDanhGiaPhim.Data;
using DichVuDanhGiaPhim.Repositories;
using DichVuDanhGiaPhim.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DanhGiaPhimDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IDanhGiaPhimRepository, DanhGiaPhimRepository>();
builder.Services.AddScoped<IDanhGiaPhimService, DanhGiaPhimService>();

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

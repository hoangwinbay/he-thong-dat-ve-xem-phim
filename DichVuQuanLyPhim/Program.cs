using Microsoft.EntityFrameworkCore;
using DichVuQuanLyPhim.Data;
using DichVuQuanLyPhim.Repositories;
using DichVuQuanLyPhim.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình kết nối CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PhimDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IPhimRepository, PhimRepository>();
builder.Services.AddScoped<IPhimService, PhimService>();

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

using Microsoft.EntityFrameworkCore;
using DichVuQuanLyGhe.Data;
using DichVuQuanLyGhe.Repositories;
using DichVuQuanLyGhe.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình kết nối CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GheDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IGheRepository, GheRepository>();
builder.Services.AddScoped<IGheService, GheService>();

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

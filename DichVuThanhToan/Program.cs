using Microsoft.EntityFrameworkCore;
using DichVuThanhToan.Data;
using DichVuThanhToan.Repositories;
using DichVuThanhToan.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ThanhToanDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IThanhToanRepository, ThanhToanRepository>();
builder.Services.AddScoped<IThanhToanService, ThanhToanService>();

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

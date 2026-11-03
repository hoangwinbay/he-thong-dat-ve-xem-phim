using Microsoft.EntityFrameworkCore;
using DichVuQuanLyRap.Data;
using DichVuQuanLyRap.Repositories;
using DichVuQuanLyRap.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình kết nối CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RapDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IRapRepository, RapRepository>();
builder.Services.AddScoped<IRapService, RapService>();

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

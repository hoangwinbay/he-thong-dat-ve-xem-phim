using Microsoft.EntityFrameworkCore;
using DichVuDatVe.Data;
using DichVuDatVe.Repositories;
using DichVuDatVe.Services;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CSDL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatVeDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection
builder.Services.AddScoped<IDatVeRepository, DatVeRepository>();
builder.Services.AddScoped<IDatVeService, DatVeService>();

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

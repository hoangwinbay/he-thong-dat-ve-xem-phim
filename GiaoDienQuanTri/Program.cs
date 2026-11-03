using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using GiaoDienQuanTri.Data;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Database Context kết nối SQL Server ADMIN-PC
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdminDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Phục vụ ảnh poster phim trực tiếp từ folder Ảnh/ ở thư mục gốc
var anhPath = Path.Combine(builder.Environment.ContentRootPath, "..", "Ảnh");
if (Directory.Exists(anhPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(anhPath),
        RequestPath = "/Ảnh"
    });
}

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TrangChu}/{action=Index}/{id?}");

app.Run();

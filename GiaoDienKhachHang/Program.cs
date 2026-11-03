using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using GiaoDienKhachHang.Data;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Database Context kết nối SQL Server ADMIN-PC
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(connectionString));

// Cấu hình HttpContextAccessor phục vụ Session trong Layout View
builder.Services.AddHttpContextAccessor();

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình MVC Controllers with Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình pipeline HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Phục vụ ảnh poster phim trực tiếp từ folder Ảnh/ ở thư mục gốc giải pháp
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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TrangChu}/{action=Index}/{id?}");

app.Run();

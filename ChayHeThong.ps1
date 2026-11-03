# PowerShell script khoi chay he thong
Write-Host "=======================================================================" -ForegroundColor Cyan
Write-Host "     KHOI CHAY HE THONG DAT VE XEM PHIM MICROSERVICES (CINEBOOKING)" -ForegroundColor Yellow
Write-Host "=======================================================================" -ForegroundColor Cyan

$root = $PSScriptRoot

$services = @(
    @{ Name = "CongKetNoiAPI"; Dir = "CongKetNoiAPI"; Port = 5000 },
    @{ Name = "DichVuQuanLyNguoiDung"; Dir = "DichVuQuanLyNguoiDung"; Port = 5244 },
    @{ Name = "DichVuQuanLyPhim"; Dir = "DichVuQuanLyPhim"; Port = 5081 },
    @{ Name = "DichVuQuanLyRap"; Dir = "DichVuQuanLyRap"; Port = 5084 },
    @{ Name = "DichVuQuanLySuatChieu"; Dir = "DichVuQuanLySuatChieu"; Port = 5053 },
    @{ Name = "DichVuQuanLyGhe"; Dir = "DichVuQuanLyGhe"; Port = 5241 },
    @{ Name = "GiaoDienQuanTri"; Dir = "GiaoDienQuanTri"; Port = 5074 },
    @{ Name = "GiaoDienKhachHang"; Dir = "GiaoDienKhachHang"; Port = 5254 }
)

foreach ($s in $services) {
    Write-Host "[-] Dang khoi chay $($s.Name) (Port $($s.Port))..." -ForegroundColor Green
    $projectPath = Join-Path $root $s.Dir
    $cmd = "Set-Location -Path '$projectPath'; dotnet run"
    Start-Process powershell.exe -ArgumentList "-NoExit", "-Command", $cmd
    Start-Sleep -Milliseconds 600
}

Write-Host ""
Write-Host "=======================================================================" -ForegroundColor Cyan
Write-Host "  TAT CA 8 DICH VU DA DUOC KHOI CHAY TRONG CAC CUA SO POWERSHELL!" -ForegroundColor Green
Write-Host "  - API Gateway Console:       http://localhost:5000" -ForegroundColor White
Write-Host "  - Giao dien Khach hang:      http://localhost:5254" -ForegroundColor White
Write-Host "  - Giao dien Quan tri:        http://localhost:5074" -ForegroundColor White
Write-Host "=======================================================================" -ForegroundColor Cyan

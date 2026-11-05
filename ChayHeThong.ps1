# PowerShell script khoi chay he thong
Write-Host "=======================================================================" -ForegroundColor Cyan
Write-Host "     KHOI CHAY HE THONG DAT VE XEM PHIM MICROSERVICES (CINEBOOKING)" -ForegroundColor Yellow
Write-Host "=======================================================================" -ForegroundColor Cyan

$root = $PSScriptRoot

$services = @(
    @{ Name = "CongKetNoiAPI"; Port = 5000 },
    @{ Name = "DichVuQuanLyNguoiDung"; Port = 5244 },
    @{ Name = "DichVuQuanLyPhim"; Port = 5081 },
    @{ Name = "DichVuQuanLyRap"; Port = 5084 },
    @{ Name = "DichVuQuanLySuatChieu"; Port = 5053 },
    @{ Name = "DichVuQuanLyGhe"; Port = 5241 },
    @{ Name = "DichVuDatVe"; Port = 5086 },
    @{ Name = "DichVuThanhToan"; Port = 5087 },
    @{ Name = "DichVuQuanLyVeDienTu"; Port = 5088 },
    @{ Name = "DichVuThongBao"; Port = 5089 },
    @{ Name = "DichVuDanhGiaPhim"; Port = 5090 },
    @{ Name = "GiaoDienQuanTri"; Port = 5074 },
    @{ Name = "GiaoDienKhachHang"; Port = 5254 }
)

foreach ($s in $services) {
    Write-Host "[-] Dang khoi chay $($s.Name) (Port $($s.Port))..." -ForegroundColor Green
    $projectDir = Join-Path $root $s.Name
    $title = "$($s.Name) (Port $($s.Port))"
    $cmd = "`$host.UI.RawUI.WindowTitle = '$title'; Set-Location -LiteralPath '$projectDir'; dotnet run --no-build"
    Start-Process powershell.exe -ArgumentList "-NoExit", "-Command", $cmd
    Start-Sleep -Milliseconds 400
}

Write-Host ""
Write-Host "=======================================================================" -ForegroundColor Cyan
Write-Host "  TAT CA CAC DICH VU DA DUOC KHOI CHAY TRONG CAC CUA SO POWERSHELL!" -ForegroundColor Green
Write-Host "  - API Gateway Console:       http://localhost:5000" -ForegroundColor White
Write-Host "  - Giao dien Khach hang:      http://localhost:5254" -ForegroundColor White
Write-Host "  - Giao dien Quan tri:        http://localhost:5074" -ForegroundColor White
Write-Host "  - Dịch vu Dat Ve (API):      http://localhost:5086/api/DatVe" -ForegroundColor White
Write-Host "  - Dịch vu Thanh Toan (API):  http://localhost:5087/api/ThanhToan" -ForegroundColor White
Write-Host "  - Dịch vu Ve Dien Tu (API):  http://localhost:5088/api/VeDienTu" -ForegroundColor White
Write-Host "  - Dịch vu Thong Bao (API):   http://localhost:5089/api/ThongBao" -ForegroundColor White
Write-Host "  - Dịch vu Danh Gia (API):    http://localhost:5090/api/DanhGiaPhim" -ForegroundColor White
Write-Host "=======================================================================" -ForegroundColor Cyan

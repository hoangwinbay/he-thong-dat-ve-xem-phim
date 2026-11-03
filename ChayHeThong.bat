@echo off
chcp 65001 >nul
echo =======================================================================
echo          KHỞI CHẠY HỆ THỐNG ĐẶT VÉ XEM PHIM MICROSERVICES
echo =======================================================================
echo.
echo [1/8] Đang khởi chạy CongKetNoiAPI (API Gateway - Port 5000)...
start "1. API Gateway (CongKetNoiAPI)" cmd /k "title API Gateway (Port 5000) && cd /d %~dp0CongKetNoiAPI && dotnet run"

timeout /t 2 /nobreak >nul

echo [2/8] Đang khởi chạy DichVuQuanLyNguoiDung (Port 5244)...
start "2. Dịch vụ Người Dùng (Port 5244)" cmd /k "title DichVuQuanLyNguoiDung (Port 5244) && cd /d %~dp0DichVuQuanLyNguoiDung && dotnet run"

echo [3/8] Đang khởi chạy DichVuQuanLyPhim (Port 5081)...
start "3. Dịch vụ Phim (Port 5081)" cmd /k "title DichVuQuanLyPhim (Port 5081) && cd /d %~dp0DichVuQuanLyPhim && dotnet run"

echo [4/8] Đang khởi chạy DichVuQuanLyRap (Port 5084)...
start "4. Dịch vụ Rạp Chiếu (Port 5084)" cmd /k "title DichVuQuanLyRap (Port 5084) && cd /d %~dp0DichVuQuanLyRap && dotnet run"

echo [5/8] Đang khởi chạy DichVuQuanLySuatChieu (Port 5053)...
start "5. Dịch vụ Suất Chiếu (Port 5053)" cmd /k "title DichVuQuanLySuatChieu (Port 5053) && cd /d %~dp0DichVuQuanLySuatChieu && dotnet run"

echo [6/8] Đang khởi chạy DichVuQuanLyGhe (Port 5241)...
start "6. Dịch vụ Ghế Ngồi (Port 5241)" cmd /k "title DichVuQuanLyGhe (Port 5241) && cd /d %~dp0DichVuQuanLyGhe && dotnet run"

timeout /t 3 /nobreak >nul

echo [7/8] Đang khởi chạy GiaoDienQuanTri (Admin Portal - Port 5074)...
start "7. Giao diện Quản trị (Port 5074)" cmd /k "title GiaoDienQuanTri (Port 5074) && cd /d %~dp0GiaoDienQuanTri && dotnet run"

echo [8/8] Đang khởi chạy GiaoDienKhachHang (Customer Portal - Port 5254)...
start "8. Giao diện Khách hàng (Port 5254)" cmd /k "title GiaoDienKhachHang (Port 5254) && cd /d %~dp0GiaoDienKhachHang && dotnet run"

echo.
echo =======================================================================
echo  TẤT CẢ 8 PHÂN HỆ ĐÃ ĐƯỢC MỞ TRONG CÁC CỬA SỔ RIÊNG BIỆT!
echo.
echo  - API Gateway Console:      http://localhost:5000
echo  - Giao diện Khách hàng:     http://localhost:5254
echo  - Giao diện Quản trị:       http://localhost:5074
echo =======================================================================
pause


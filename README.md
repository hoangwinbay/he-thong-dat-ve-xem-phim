# CINEBOOKING - Hệ Thống Đặt Vé Xem Phim Trực Tuyến

Hệ thống đặt vé xem phim trực tuyến hiện đại được thiết kế theo kiến trúc **Microservices** trên nền tảng **ASP.NET Core (.NET 8)**, cơ sở dữ liệu **Microsoft SQL Server**, bao gồm API Gateway và 2 phân hệ giao diện người dùng.

---

## 🏛️ Kiến Trúc Hệ Thống

```
HeThongDatVeXemPhim/
├── Ảnh/                                      # Thư mục lưu trữ 26 poster phim thực tế (400x600px)
├── CSDL_DatVeXemPhim_InsertData.sql         # Script T-SQL nạp dữ liệu đầy đủ 15 bảng (>=25 dòng/bảng)
├── HeThongDatVeXemPhim.sln                  # Solution tổng quản lý toàn bộ hệ thống
│
├── CỔNG KẾT NỐI API (API GATEWAY):
│   └── CongKetNoiAPI/                        # Cổng API Gateway tập trung (Port 5000 / HTTPS 7000)
│       ├── Reverse Proxy động
│       ├── API Aggregator Pattern (Phim + Suất chiếu + Ghế)
│       └── Bảng điều khiển trực quan (Console Dashboard)
│
├── 10 DỊCH VỤ MICROSERVICES (BACKEND):
│   ├── DichVuQuanLyNguoiDung/                # 1. Đăng ký, đăng nhập, phân quyền (Port 5244)
│   ├── DichVuQuanLyPhim/                     # 2. Quản lý 26 phim, poster, trailer, thể loại (Port 5081)
│   ├── DichVuQuanLyRap/                      # 3. Cụm rạp chiếu, địa chỉ, chi nhánh, phòng chiếu (Port 5084)
│   ├── DichVuQuanLySuatChieu/                # 4. Quản lý lịch chiếu, ngày giờ, giá vé (Port 5053)
│   ├── DichVuQuanLyGhe/                      # 5. Sơ đồ ghế, loại ghế (VIP/Đôi), giữ chỗ, đặt vé (Port 5241)
│   ├── DichVuDatVe/                          # 6. Tiếp nhận đặt vé, kiểm tra ghế, tính tiền, tạo đơn (Port 5086)
│   ├── DichVuThanhToan/                      # 7. Xử lý thanh toán, đối soát GD, cập nhật trạng thái (Port 5087)
│   ├── DichVuQuanLyVeDienTu/                 # 8. Phát hành vé điện tử & Soát vé qua mã QR (Port 5088)
│   ├── DichVuThongBao/                       # 9. Gửi thông báo xác nhận đặt vé, khuyến mãi (Port 5089)
│   └── DichVuDanhGiaPhim/                    # 10. Đánh giá số sao (1-5 ⭐) & Bình luận phim (Port 5090)
│
├── 2 PHÂN HỆ GIAO DIỆN (FRONTEND & ADMIN UI):
│   ├── GiaoDienKhachHang/                    # Web Portal Khách hàng đặt vé xem phim (Port 5254)
│   │   ├── Banner carousel phim hot, phim đang chiếu & sắp chiếu
│   │   ├── Lọc phim theo thể loại, độ tuổi, tìm kiếm
│   │   ├── Lịch chiếu phim theo rạp và ngày
│   │   ├── Sơ đồ chọn ghế ma trận trực quan & tính tiền thời gian thực
│   │   ├── Trang xác nhận đặt vé chi tiết & tính tổng tiền
│   │   ├── Cổng thanh toán trực tuyến (VNPAY, MoMo, Thẻ quốc tế, Tiền mặt)
│   │   ├── Lịch sử đặt vé và theo dõi trạng thái đơn hàng
│   │   ├── Xem vé điện tử & Mã QR quét vào rạp, in vé (Dịch vụ 8)
│   │   ├── Hộp thư thông báo cá nhân & cập nhật giao dịch (Dịch vụ 9)
│   │   └── Xem & Gửi đánh giá sao (1-5 ⭐), bình luận phim (Dịch vụ 10)
│   └── GiaoDienQuanTri/                      # Web Portal Quản trị hệ thống (Port 5074)
│       ├── Dashboard thống kê tổng quan
│       ├── Quản trị người dùng, phim, thể loại
│       ├── Quản lý cụm rạp, phòng chiếu
│       ├── Quản lý suất chiếu & xem sơ đồ trạng thái ghế theo suất chiếu
│       ├── Quản lý đơn đặt vé (Dịch vụ 6): Xem chi tiết, lọc trạng thái, cập nhật trạng thái đơn
│       ├── Quản lý thanh toán (Dịch vụ 7): Thống kê doanh thu, đối soát mã GD, hoàn tiền hủy vé
│       ├── Quản lý vé điện tử & Quầy soát vé/Check-in tự động (Dịch vụ 8)
│       ├── Quản lý thông báo & Soạn phát sóng tin tức khách hàng (Dịch vụ 9)
│       └── Quản lý đánh giá & Kiểm duyệt bình luận phim (Dịch vụ 10)
│
└── CÁC SCRIPT TIỆN ÍCH:
    ├── ChayHeThong.bat                       # Khởi chạy toàn bộ hệ thống bằng 1 click (Windows CMD)
    └── ChayHeThong.ps1                       # Khởi chạy toàn bộ hệ thống bằng PowerShell
```

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Hệ Thống

### 1. Chuẩn Bị Cơ Sở Dữ Liệu SQL Server
- Mở **SQL Server Management Studio (SSMS)**.
- Chạy script tạo cơ sở dữ liệu `CSDL_DatVeXemPhim` và nạp toàn bộ dữ liệu mẫu từ file:
  `CSDL_DatVeXemPhim_InsertData.sql`

### 2. Khởi Chạy Nhanh Toàn Bộ Hệ Thống (Khuyến nghị)
Nhấp đúp chuột vào file:
```cmd
ChayHeThong.bat
```
hoặc chạy bằng PowerShell:
```powershell
.\ChayHeThong.ps1
```

### 3. Truy Cập Các Phân Hệ
| Ứng Dụng | URL Truy Cập | Mô Tả |
|:---|:---:|:---|
| **API Gateway Console** | [http://localhost:5000](http://localhost:5000) | Bảng điều khiển kiểm tra trạng thái sức khỏe & Ping microservices |
| **Giao Diện Khách Hàng** | [http://localhost:5254](http://localhost:5254) | Cổng đặt vé xem phim trực tuyến |
| **Giao Diện Quản Trị** | [http://localhost:5074](http://localhost:5074) | Cổng quản trị viên điều hành hệ thống rạp |
| **Dịch vụ Người dùng** | [http://localhost:5244/api/QuanLyNguoiDung](http://localhost:5244/api/QuanLyNguoiDung) | API Quản lý Người dùng |
| **Dịch vụ Phim** | [http://localhost:5081/api/QuanLyPhim](http://localhost:5081/api/QuanLyPhim) | API Quản lý Phim |
| **Dịch vụ Rạp chiếu** | [http://localhost:5084/api/QuanLyRap](http://localhost:5084/api/QuanLyRap) | API Quản lý Rạp chiếu |
| **Dịch vụ Suất chiếu** | [http://localhost:5053/api/QuanLySuatChieu](http://localhost:5053/api/QuanLySuatChieu) | API Quản lý Suất chiếu |
| **Dịch vụ Ghế ngồi** | [http://localhost:5241/api/QuanLyGhe/phong/1](http://localhost:5241/api/QuanLyGhe/phong/1) | API Quản lý Ghế ngồi |
| **Dịch vụ Đặt vé** | [http://localhost:5086/api/DatVe](http://localhost:5086/api/DatVe) | API Quản lý & Tạo Đơn Đặt Vé |
| **Dịch vụ Thanh toán** | [http://localhost:5087/api/ThanhToan](http://localhost:5087/api/ThanhToan) | API Xử lý Thanh toán & Đối soát GD |
| **Dịch vụ Vé điện tử** | [http://localhost:5088/api/VeDienTu](http://localhost:5088/api/VeDienTu) | API Tạo Vé Điện Tử & Soát Vé QR Code |
| **Dịch vụ Thông báo** | [http://localhost:5089/api/ThongBao](http://localhost:5089/api/ThongBao) | API Gửi Thông Báo Cá Nhân & Phát Sóng |
| **Dịch vụ Đánh giá phim** | [http://localhost:5090/api/DanhGiaPhim](http://localhost:5090/api/DanhGiaPhim) | API Đánh Giá Sao (1-5 ⭐) & Bình Luận |

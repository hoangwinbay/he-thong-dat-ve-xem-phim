-- =========================================================================
-- BỘ DỮ LIỆU MẪU ĐẦY ĐỦ CHO HỆ THỐNG ĐẶT VÉ XEM PHIM (CSDL_DatVeXemPhim)
-- Tối thiểu mỗi bảng có 25 bản ghi
-- Ảnh poster phim tham chiếu tới thư mục 'Ảnh/'
-- Chuẩn SQL Server (T-SQL), hỗ trợ Unicode (N'...')
-- =========================================================================

USE CSDL_DatVeXemPhim;
GO

SET NOCOUNT ON;
GO

-- =========================================================================
-- 0. DỌN DẸP DỮ LIỆU CŨ THEO THỨ TỰ NGƯỢC RÀNG BUỘC KHÓA NGOẠI
-- =========================================================================
PRINT N'Đang dọn dẹp dữ liệu cũ (nếu có)...';

DELETE FROM VeDienTu;
DELETE FROM ChiTietDatVe;
DELETE FROM ThanhToan;
DELETE FROM DatVe;
DELETE FROM GheSuatChieu;
DELETE FROM SuatChieu;
DELETE FROM GheNgoi;
DELETE FROM PhongChieu;
DELETE FROM RapChieu;
DELETE FROM Phim_TheLoai;
DELETE FROM DanhGiaPhim;
DELETE FROM ThongBao;
DELETE FROM Phim;
DELETE FROM TheLoai;
DELETE FROM NguoiDung;
GO

PRINT N'Dọn dẹp hoàn tất. Bắt đầu chèn dữ liệu...';
GO

-- =========================================================================
-- 1. BẢNG NguoiDung (28 bản ghi >= 25)
-- Vai trò: QuanTri, NhanVien, KhachHang
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng NguoiDung...';
SET IDENTITY_INSERT NguoiDung ON;

INSERT INTO NguoiDung (MaNguoiDung, HoTen, Email, MatKhau, SoDienThoai, VaiTro, NgayTao, TrangThai) VALUES
(1, N'Nguyễn Văn An', 'nguyenvanan.admin@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0901234567', N'QuanTri', '2024-01-01 08:00:00', 1),
(2, N'Trần Thị Mai', 'tranthimai.admin@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0902345678', N'QuanTri', '2024-01-02 09:30:00', 1),
(3, N'Lê Hoàng Long', 'lehoanglong.staff@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0912345671', N'NhanVien', '2024-01-05 10:00:00', 1),
(4, N'Phạm Minh Thảo', 'phamminhthao.staff@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0912345672', N'NhanVien', '2024-01-05 10:15:00', 1),
(5, N'Đỗ Đức Thịnh', 'doducthinh.staff@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0912345673', N'NhanVien', '2024-01-06 14:00:00', 1),
(6, N'Vũ Thúy Kiều', 'vuthuykieu.staff@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0912345674', N'NhanVien', '2024-01-06 15:30:00', 1),
(7, N'Bùi Tuấn Anh', 'buituananh@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0981112233', N'KhachHang', '2024-01-10 09:00:00', 1),
(8, N'Hoàng Ngọc Ánh', 'hoangngocanh@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0982223344', N'KhachHang', '2024-01-11 11:20:00', 1),
(9, N'Ngô Gia Bảo', 'ngogiabao@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0983334455', N'KhachHang', '2024-01-12 14:10:00', 1),
(10, N'Dương Thùy Chi', 'duongthuychi@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0984445566', N'KhachHang', '2024-01-13 16:45:00', 1),
(11, N'Đinh Trọng Dũng', 'dinhtrongdung@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0985556677', N'KhachHang', '2024-01-14 18:30:00', 1),
(12, N'Trịnh Mỹ Duyên', 'trinhmyduyen@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0986667788', N'KhachHang', '2024-01-15 20:00:00', 1),
(13, N'Lý Hải Đăng', 'lyhaidang@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0987778899', N'KhachHang', '2024-01-16 08:15:00', 1),
(14, N'Mai Phương Hoa', 'maiphuonghoa@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0988889900', N'KhachHang', '2024-01-17 10:40:00', 1),
(15, N'Phan Thanh Hùng', 'phanthanhhung@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0989990011', N'KhachHang', '2024-01-18 13:50:00', 1),
(16, N'Võ Quốc Huy', 'voquochuy@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0971112244', N'KhachHang', '2024-01-19 15:25:00', 1),
(17, N'Cao Thị Kim Khuyên', 'caothikimkhuyen@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0972223355', N'KhachHang', '2024-01-20 17:10:00', 1),
(18, N'Hồ Quang Lâm', 'hoquanglam@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0973334466', N'KhachHang', '2024-01-21 19:35:00', 1),
(19, N'Nguyễn Khánh Linh', 'nguyenkhanhlinh@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0974445577', N'KhachHang', '2024-01-22 21:00:00', 1),
(20, N'Trần Công Minh', 'trancongminh@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0975556688', N'KhachHang', '2024-01-23 07:45:00', 1),
(21, N'Đặng Bảo Ngọc', 'dangbaongoc@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0976667799', N'KhachHang', '2024-01-24 09:15:00', 1),
(22, N'Hà Minh Nhật', 'haminhnhat@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0977778800', N'KhachHang', '2024-01-25 11:30:00', 1),
(23, N'Lâm Vĩnh Phát', 'lamvinhphat@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0978889911', N'KhachHang', '2024-01-26 14:20:00', 1),
(24, N'Quách Như Quỳnh', 'quachnhuquynh@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0979990022', N'KhachHang', '2024-01-27 16:50:00', 1),
(25, N'Tạ Đình Sang', 'tadinhsang@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0961112255', N'KhachHang', '2024-01-28 18:40:00', 1),
(26, N'Uông Thanh Tâm', 'uongthanhtam@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0962223366', N'KhachHang', '2024-01-29 20:10:00', 1),
(27, N'Vương Anh Uyên', 'vuonganhuyen@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0963334477', N'KhachHang', '2024-01-30 08:30:00', 1),
(28, N'Đoàn Tiến Dũng', 'doantiendung@gmail.com', '$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa', '0964445588', N'KhachHang', '2024-01-31 10:00:00', 1);
SET IDENTITY_INSERT NguoiDung OFF;
GO

-- =========================================================================
-- 2. BẢNG TheLoai (26 bản ghi >= 25)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng TheLoai...';
SET IDENTITY_INSERT TheLoai ON;

INSERT INTO TheLoai (MaTheLoai, TenTheLoai) VALUES
(1, N'Hành Động'),
(2, N'Phiêu Lưu'),
(3, N'Hoạt Hình'),
(4, N'Hài Hước'),
(5, N'Tội Phạm'),
(6, N'Tài Liệu'),
(7, N'Chính Kịch'),
(8, N'Gia Đình'),
(9, N'Giả Tưởng'),
(10, N'Lịch Sử'),
(11, N'Kinh Dị'),
(12, N'Âm Nhạc'),
(13, N'Bí Ẩn'),
(14, N'Lãng Mạn'),
(15, N'Khoa Học Viễn Tưởng'),
(16, N'Thể Thao'),
(17, N'Giật Gân'),
(18, N'Chiến Tranh'),
(19, N'Miền Tây'),
(20, N'Võ Thuật'),
(21, N'Tâm Lý'),
(22, N'Siêu Anh Hùng'),
(23, N'Thần Thoại'),
(24, N'Trinh Thám'),
(25, N'Cổ Trang'),
(26, N'Thảm Họa');
SET IDENTITY_INSERT TheLoai OFF;
GO

-- =========================================================================
-- 3. BẢNG Phim (26 bản ghi >= 25)
-- Ảnh poster lưu trong folder 'Ảnh/' (vd: Ảnh/poster_mai.jpg)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng Phim...';
SET IDENTITY_INSERT Phim ON;

INSERT INTO Phim (MaPhim, TenPhim, MoTa, DaoDien, DienVien, ThoiLuong, NgayKhoiChieu, NgayKetThuc, QuocGia, DoTuoi, Poster, Trailer, TrangThai) VALUES
(1, N'Mai', N'Câu chuyện xoay quanh Mai, một phụ nữ gần 40 tuổi làm nghề massage, phải đối mặt với định kiến xã hội và tìm kiếm hạnh phúc đích thực.', N'Trấn Thành', N'Phương Anh Đào, Tuấn Trần, Trấn Thành, Hồng Đào', 131, '2024-02-10', '2024-04-10', N'Việt Nam', N'T18', N'Ảnh/poster_mai.jpg', N'https://www.youtube.com/watch?v=mai_trailer', N'NgungChieu'),
(2, N'Lật Mặt 7: Một Điều Ước', N'Khắc họa tình cảm gia đình cảm động của người mẹ già cùng 5 người con trưởng thành với những hoàn cảnh và số phận khác nhau.', N'Lý Hải', N'Thanh Hiền, Trương Minh Cường, Đinh Y Nhung, Quách Ngọc Tuyên', 138, '2024-04-26', '2024-06-30', N'Việt Nam', N'K', N'Ảnh/poster_lat_mat_7.jpg', N'https://www.youtube.com/watch?v=latmat7_trailer', N'NgungChieu'),
(3, N'Đào, Phở và Piano', N'Bức tranh bi tráng và lãng mạn về tình yêu thời chiến trong bối cảnh cuộc chiến đấu bảo vệ Hà Nội mùa đông năm 1946.', N'Phi Tiến Sơn', N'Doãn Quốc Đam, Cao Thị Thùy Linh, Trần Lực, Tuấn Hưng', 100, '2024-02-10', '2024-04-15', N'Việt Nam', N'T13', N'Ảnh/poster_dao_pho_piano.jpg', N'https://www.youtube.com/watch?v=daophopiano_trailer', N'NgungChieu'),
(4, N'Nhà Bà Nữ', N'Mâu thuẫn thế hệ và áp lực gia đình trong một tiệm bánh canh cua nổi tiếng tại Sài Gòn do một người mẹ độc đoán làm chủ.', N'Trấn Thành', N'Lê Giang, Uyển Ân, Song Luân, Trấn Thành, Khả Như', 102, '2023-01-22', '2023-03-30', N'Việt Nam', N'T16', N'Ảnh/poster_nha_ba_nu.jpg', N'https://www.youtube.com/watch?v=nhabanu_trailer', N'NgungChieu'),
(5, N'Bố Già', N'Câu chuyện tình cha con đầy xúc động của Ba Sang - người đàn ông luôn lo chuyện bao đồng và con trai Quắn - YouTuber hiện đại.', N'Vũ Ngọc Đãng, Trấn Thành', N'Trấn Thành, Tuấn Trần, Ngân Chi, NSND Ngọc Giàu', 128, '2021-03-05', '2021-05-15', N'Việt Nam', N'P', N'Ảnh/poster_bo_gia.jpg', N'https://www.youtube.com/watch?v=bogia_trailer', N'NgungChieu'),
(6, N'Mắt Biếc', N'Mối tình đơn phương da diết của Ngạn dành cho cô bạn thân Hà Lan với đôi mắt biếc biếc qua bao thăng trầm tuổi thanh xuân.', N'Victor Vũ', N'Trần Nghĩa, Trúc Anh, Trần Phong, Đỗ Khánh Vân', 117, '2019-12-20', '2020-02-28', N'Việt Nam', N'P', N'Ảnh/poster_mat_biec.jpg', N'https://www.youtube.com/watch?v=matbiec_trailer', N'NgungChieu'),
(7, N'Hai Phượng', N'Hành trình nghẹt thở của một nữ giang hồ quy ẩn truy đuổi băng nhóm buôn bán trẻ em để giải cứu con gái bị bắt cóc.', N'Lê Văn Kiệt', N'Ngô Thanh Vân, Cát Vy, Phan Thanh Nhiên, Phạm Anh Khoa', 98, '2019-02-22', '2019-04-30', N'Việt Nam', N'T18', N'Ảnh/poster_hai_phuong.jpg', N'https://www.youtube.com/watch?v=haiphuong_trailer', N'NgungChieu'),
(8, N'Em Chưa 18', N'Câu chuyện tình yêu hài hước, dở khóc dở cười giữa tay chơi Hoàng hào hoa và cô nàng học sinh cá tính Linh Đan.', N'Lê Thanh Sơn', N'Kaity Nguyễn, Kiều Minh Tuấn, Will, Châu Bùi', 95, '2017-04-28', '2017-06-30', N'Việt Nam', N'T16', N'Ảnh/poster_em_chua_18.jpg', N'https://www.youtube.com/watch?v=emchua18_trailer', N'NgungChieu'),
(9, N'Quỷ Cẩu', N'Lấy cảm hứng từ truyền thuyết linh dị dân gian Chó đội nón mê, kể về những tai ương giáng xuống một gia đình làm nghề mổ chó.', N'Lưu Thành Luân', N'Quang Tuấn, Nam Thư, Kim Xuân, Vân Dung, Quốc Quân', 108, '2023-12-29', '2024-03-01', N'Việt Nam', N'T18', N'Ảnh/poster_quy_cau.jpg', N'https://www.youtube.com/watch?v=quycau_trailer', N'NgungChieu'),
(10, N'Kẻ Ăn Hồn', N'Cổ thuật Rượu Sọ Người reo rắc kinh hoàng lên Làng Địa Ngục cổ xưa, hé lộ nguồn gốc lời nguyền gia tộc đẫm máu.', N'Trần Hữu Tấn', N'Hoàng Hà, Võ Điền Gia Huy, Huỳnh Thanh Trực, Lan Phương', 109, '2023-12-15', '2024-02-15', N'Việt Nam', N'T18', N'Ảnh/poster_ke_an_hon.jpg', N'https://www.youtube.com/watch?v=keanhon_trailer', N'NgungChieu'),
(11, N'Dune: Hành Tinh Cát 2', N'Paul Atreides hợp lực cùng Chani và tộc người Fremen để trả thù những kẻ đã hủy hoại gia đình mình và định đoạt số phận vũ trụ.', N'Denis Villeneuve', N'Timothée Chalamet, Zendaya, Rebecca Ferguson, Javier Bardem', 166, '2024-03-01', '2024-05-15', N'Mỹ', N'T16', N'Ảnh/poster_dune_2.jpg', N'https://www.youtube.com/watch?v=dune2_trailer', N'DangChieu'),
(12, N'Oppenheimer', N'Tác phẩm tiểu sử lịch sử khắc họa cuộc đời của J. Robert Oppenheimer, cha đẻ của bom nguyên tử trong Dự án Manhattan.', N'Christopher Nolan', N'Cillian Murphy, Emily Blunt, Matt Damon, Robert Downey Jr.', 180, '2023-08-11', '2023-11-10', N'Mỹ', N'T18', N'Ảnh/poster_oppenheimer.jpg', N'https://www.youtube.com/watch?v=oppenheimer_trailer', N'NgungChieu'),
(13, N'Godzilla x Kong: Đế Chế Mới', N'Hai quái thú khổng lồ Godzilla và Kong phải hợp sức đối đầu với một mối đe dọa sinh tử chưa từng được biết đến từ Trái Đất Rỗng.', N'Adam Wingard', N'Rebecca Hall, Brian Tyree Henry, Dan Stevens, Kaylee Hottle', 115, '2024-03-29', '2024-05-30', N'Mỹ', N'T13', N'Ảnh/poster_godzilla_kong.jpg', N'https://www.youtube.com/watch?v=godzillakong_trailer', N'DangChieu'),
(14, N'Kung Fu Panda 4', N'Po chuẩn bị trở thành Thủ lĩnh Tinh thần của Thung lũng Bình Yên và phải tìm kiếm một Thần Long Đại Hiệp mới để kế vị.', N'Mike Mitchell', N'Jack Black, Awkwafina, Viola Davis, Dustin Hoffman', 94, '2024-03-08', '2024-05-20', N'Mỹ', N'P', N'Ảnh/poster_kungfu_panda_4.jpg', N'https://www.youtube.com/watch?v=kungfupanda4_trailer', N'DangChieu'),
(15, N'Thám Tử Lừng Danh Conan: Ngôi Sao 5 Cánh 1 Triệu Đô', N'Conan, Heiji Hattori và Siêu trộm Kaito Kid bước vào cuộc truy tìm kho báu bí ẩn liên quan đến thanh kiếm Nhật cổ tại Hakodate.', N'Chika Nagaoka', N'Minami Takayama, Wakana Yamazaki, Kappei Yamaguchi, Ryo Horikawa', 111, '2024-08-02', '2024-10-15', N'Nhật Bản', N'K', N'Ảnh/poster_conan_ngoi_sao.jpg', N'https://www.youtube.com/watch?v=conan_trailer', N'DangChieu'),
(16, N'Doraemon: Bản Giao Hưởng Địa Cầu', N'Doraemon, Nobita và nhóm bạn bắt đầu chuyến phiêu lưu âm nhạc kỳ thú để giải cứu Trái Đất khỏi một sinh vật đe dọa nuốt chửng âm thanh.', N'Kazuaki Imai', N'Wasabi Mizuta, Megumi Ohara, Yumi Kakazu, Subaru Kimura', 115, '2024-05-24', '2024-07-31', N'Nhật Bản', N'P', N'Ảnh/poster_doraemon_giao_huong.jpg', N'https://www.youtube.com/watch?v=doraemon_trailer', N'DangChieu'),
(17, N'Avatar: Dòng Chảy Của Nước', N'Gia đình Jake Sully và Neytiri phải rời khỏi khu rừng thân thuộc và hòa nhập với bộ tộc Metkayina vùng đại dương trên hành tinh Pandora.', N'James Cameron', N'Sam Worthington, Zoe Saldana, Sigourney Weaver, Stephen Lang', 192, '2022-12-16', '2023-03-15', N'Mỹ', N'T13', N'Ảnh/poster_avatar_2.jpg', N'https://www.youtube.com/watch?v=avatar2_trailer', N'NgungChieu'),
(18, N'Spider-Man: Không Còn Nhà', N'Danh tính Peter Parker bị lộ dẫn đến việc cậu nhờ Doctor Strange can thiệp ma thuật, vô tình mở ra hiểm họa đa vũ trụ.', N'Jon Watts', N'Tom Holland, Zendaya, Benedict Cumberbatch, Willem Dafoe', 148, '2021-12-17', '2022-03-15', N'Mỹ', N'T13', N'Ảnh/poster_spiderman_no_way_home.jpg', N'https://www.youtube.com/watch?v=spiderman_trailer', N'NgungChieu'),
(19, N'Avengers: Hồi Kết', N'Sau cái búng tay hủy diệt của Thanos, các Avengers sống sót tập hợp lại để thực hiện canh bạc du hành thời gian nhằm cứu vũ trụ.', N'Anthony Russo, Joe Russo', N'Robert Downey Jr., Chris Evans, Mark Ruffalo, Chris Hemsworth', 181, '2019-04-26', '2019-07-15', N'Mỹ', N'T13', N'Ảnh/poster_avengers_endgame.jpg', N'https://www.youtube.com/watch?v=endgame_trailer', N'NgungChieu'),
(20, N'Hố Đen Tử Thần', N'Nhóm nhà khoa học dẫn đầu bởi Cooper thực hiện chuyến du hành vượt ra khỏi dải ngân hà để tìm kiếm hành tinh sống mới cho nhân loại.', N'Christopher Nolan', N'Matthew McConaughey, Anne Hathaway, Jessica Chastain, Michael Caine', 169, '2014-11-07', '2015-02-15', N'Mỹ', N'T13', N'Ảnh/poster_interstellar.jpg', N'https://www.youtube.com/watch?v=interstellar_trailer', N'NgungChieu'),
(21, N'Những Mảnh Ghép Cảm Xúc 2', N'Riley bước vào tuổi dậy thì với những cảm xúc mới xuất hiện tại trung tâm điều khiển: Lo Âu, Ganh Tị, Xấu Hổ và Chán Nản.', N'Kelsey Mann', N'Amy Poehler, Maya Hawke, Phyllis Smith, Lewis Black', 96, '2024-06-14', '2024-08-31', N'Mỹ', N'P', N'Ảnh/poster_inside_out_2.jpg', N'https://www.youtube.com/watch?v=insideout2_trailer', N'DangChieu'),
(22, N'Kẻ Trộm Mặt Trăng 4', N'Gru, Lucy và các cô con gái chào đón thành viên mới Gru Jr., đồng thời phải trốn chạy kẻ thù truyền kiếp Maxime Le Mal vượt ngục.', N'Chris Renaud', N'Steve Carell, Kristen Wiig, Will Ferrell, Sofia Vergara', 94, '2024-07-05', '2024-09-15', N'Mỹ', N'P', N'Ảnh/poster_despicable_me_4.jpg', N'https://www.youtube.com/watch?v=despicableme4_trailer', N'DangChieu'),
(23, N'Deadpool & Wolverine', N'Wade Wilson tái xuất khi tổ chức TVA lôi kéo anh vào một nhiệm vụ bảo vệ dòng thời gian cùng biến thể Logan đầy miễn cưỡng.', N'Shawn Levy', N'Ryan Reynolds, Hugh Jackman, Emma Corrin, Matthew Macfadyen', 128, '2024-07-26', '2024-10-15', N'Mỹ', N'T18', N'Ảnh/poster_deadpool_wolverine.jpg', N'https://www.youtube.com/watch?v=deadpool_trailer', N'DangChieu'),
(24, N'Quật Mộ Trùng Ma', N'Hai pháp sư, một thầy phong thủy và một chuyên gia mai táng nhận lời khai quật một ngôi mộ bí ẩn tại vùng núi hẻo lánh và giải phóng ác linh.', N'Jang Jae-hyun', N'Choi Min-sik, Kim Go-eun, Yoo Hae-jin, Lee Do-hyun', 134, '2024-03-15', '2024-05-30', N'Hàn Quốc', N'T16', N'Ảnh/poster_exhuma.jpg', N'https://www.youtube.com/watch?v=exhuma_trailer', N'DangChieu'),
(25, N'Gặp Lại Chị Bầu', N'Phúc bất ngờ xuyên không về Sài Gòn thập niên 1990 và gặp gỡ nhóm bạn trẻ nhiệt huyết cùng Huyền - người mang thai đầy bí ẩn.', N'Đoàn Nhất Trung', N'Anh Tú, Diệu Nhi, Ngọc Phước, Quốc Khánh, Lê Giang', 114, '2024-02-10', '2024-04-10', N'Việt Nam', N'T13', N'Ảnh/poster_gap_lai_chi_bau.jpg', N'https://www.youtube.com/watch?v=gaplaichibau_trailer', N'NgungChieu'),
(26, N'Mufasa: Vua Sư Tử', N'Hành trình từ chú sư tử mồ côi đơn độc vươn lên thành một vị vua huyền thoại của Vùng Đất Kiêu Hãnh cùng Taka.', N'Barry Jenkins', N'Aaron Pierre, Kelvin Harrison Jr., Seth Rogen, Billy Eichner', 118, '2024-12-20', '2025-02-28', N'Mỹ', N'P', N'Ảnh/poster_mufasa.jpg', N'https://www.youtube.com/watch?v=mufasa_trailer', N'SapChieu');
SET IDENTITY_INSERT Phim OFF;
GO

-- =========================================================================
-- 4. BẢNG Phim_TheLoai (71 bản ghi >= 25)
-- Bảng liên kết Phim và Thể loại
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng Phim_TheLoai...';

INSERT INTO Phim_TheLoai (MaPhim, MaTheLoai) VALUES
(1, 7),
(1, 14),
(1, 21),
(2, 7),
(2, 8),
(3, 10),
(3, 14),
(3, 18),
(4, 4),
(4, 7),
(4, 8),
(5, 7),
(5, 8),
(6, 7),
(6, 14),
(7, 1),
(7, 5),
(7, 20),
(8, 4),
(8, 14),
(9, 11),
(9, 13),
(10, 11),
(10, 23),
(11, 1),
(11, 2),
(11, 15),
(12, 6),
(12, 7),
(12, 10),
(13, 1),
(13, 9),
(13, 15),
(14, 1),
(14, 3),
(14, 4),
(15, 3),
(15, 13),
(15, 24),
(16, 3),
(16, 8),
(16, 12),
(17, 1),
(17, 2),
(17, 15),
(18, 1),
(18, 9),
(18, 22),
(19, 1),
(19, 15),
(19, 22),
(20, 7),
(20, 15),
(21, 3),
(21, 4),
(21, 8),
(22, 3),
(22, 4),
(22, 8),
(23, 1),
(23, 4),
(23, 22),
(24, 11),
(24, 13),
(24, 17),
(25, 4),
(25, 7),
(25, 14),
(26, 2),
(26, 3),
(26, 8);
GO

-- =========================================================================
-- 5. BẢNG RapChieu (26 bản ghi >= 25)
-- Cụm rạp CGV, Lotte, BHD, Galaxy, Beta, Cinestar,...
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng RapChieu...';
SET IDENTITY_INSERT RapChieu ON;

INSERT INTO RapChieu (MaRap, TenRap, DiaChi, SoDienThoai, TrangThai) VALUES
(1, N'CGV Vincom Center Bà Triệu', N'Tầng 6, Vincom Center, 191 Bà Triệu, Q. Hai Bà Trưng, Hà Nội', '02439743333', 1),
(2, N'CGV Vincom Center Đồng Khởi', N'Tầng 3, 72 Lê Thánh Tôn, Bến Nghé, Quận 1, TP. Hồ Chí Minh', '02838275555', 1),
(3, N'CGV Landmark 81', N'Tầng B1, Vincom Center Landmark 81, 720A Điện Biên Phủ, Bình Thạnh, TP. Hồ Chí Minh', '02836209999', 1),
(4, N'CGV Aeon Mall Long Biên', N'Tầng 4, TTTM Aeon Mall Long Biên, 27 Cổ Linh, Long Biên, Hà Nội', '02437576999', 1),
(5, N'CGV Vincom Plaza Đà Nẵng', N'Tầng 4, Vincom Plaza, 910A Ngô Quyền, Sơn Trà, TP. Đà Nẵng', '02363996888', 1),
(6, N'Lotte Cinema Keangnam', N'Tầng 5, Keangnam Landmark Tower, Phạm Hùng, Nam Từ Liêm, Hà Nội', '02438378035', 1),
(7, N'Lotte Cinema Nam Sài Gòn', N'Tầng 3, Lotte Mart, 469 Nguyễn Hữu Thọ, Tân Hưng, Quận 7, TP. Hồ Chí Minh', '02837752524', 1),
(8, N'Lotte Cinema Gò Vấp', N'Tầng 3, Lotte Mart, 242 Nguyễn Văn Lượng, Phường 10, Gò Vấp, TP. Hồ Chí Minh', '02836202151', 1),
(9, N'Lotte Cinema Cần Thơ', N'Tầng 1, Big C Cần Thơ, Lô số 1, KDC Hưng Phú 1, Cái Răng, TP. Cần Thơ', '02923917172', 1),
(10, N'Lotte Cinema Nha Trang Thái Nguyên', N'Tầng 5, Maximark cũ, 60 Thái Nguyên, Phương Sài, TP. Nha Trang', '02583824881', 1),
(11, N'BHD Star Bitexco', N'Tầng 3 & 4, Bitexco Icon 68, 2 Hải Triều, Bến Nghé, Quận 1, TP. Hồ Chí Minh', '02862670670', 1),
(12, N'BHD Star Vincom Phạm Ngọc Thạch', N'Tầng 8, Vincom Center, 2 Phạm Ngọc Thạch, Đống Đa, Hà Nội', '02436373355', 1),
(13, N'BHD Star The Garden', N'Tầng 4, TTTM The Garden, Mễ Trì, Nam Từ Liêm, Hà Nội', '02432068678', 1),
(14, N'BHD Star Discovery Cầu Giấy', N'Tầng 8, Discovery Complex, 302 Cầu Giấy, Dịch Vọng, Hà Nội', '02432069678', 1),
(15, N'Galaxy Nguyễn Du', N'116 Nguyễn Du, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh', '02838234955', 1),
(16, N'Galaxy Tràng Thi', N'10 Hai Bà Trưng, Phường Tràng Tiền, Hoàn Kiếm, Hà Nội', '02439387666', 1),
(17, N'Galaxy Tân Bình', N'246 Nguyễn Hồng Đào, Phường 14, Tân Bình, TP. Hồ Chí Minh', '02838494567', 1),
(18, N'Galaxy Sala Thủ Thiêm', N'Tầng 3, Thiso Mall, 10 Mai Chí Thọ, TP. Thủ Đức, TP. Hồ Chí Minh', '02836367890', 1),
(19, N'Trung Tâm Chiếu Phim Quốc Gia', N'87 Láng Hạ, Chợ Dừa, Ba Đình, Hà Nội', '02435141791', 1),
(20, N'Cinestar Quốc Thanh', N'271 Nguyễn Trãi, Phường Nguyễn Cư Trinh, Quận 1, TP. Hồ Chí Minh', '02873008881', 1),
(21, N'Cinestar Hai Bà Trưng', N'135 Hai Bà Trưng, Bến Nghé, Quận 1, TP. Hồ Chí Minh', '02873008882', 1),
(22, N'Cinestar Sinh Viên Bình Dương', N'Khuôn viên Nhà Văn Hóa Sinh Viên ĐHQG TP.HCM, Dĩ An, Bình Dương', '02747308883', 1),
(23, N'Beta Cinemas Đan Phượng', N'Tầng 2, Tòa nhà HHA, KĐT Tân Tây Đô, Tân Lập, Đan Phượng, Hà Nội', '0934632682', 1),
(24, N'Beta Cinemas Thanh Xuân', N'Tầng hầm B1, Tòa Golden West, 2 Lê Văn Thiêm, Thanh Xuân, Hà Nội', '0936081232', 1),
(25, N'Beta Cinemas Quang Trung', N'645 Quang Trung, Phường 11, Gò Vấp, TP. Hồ Chí Minh', '0938472912', 1),
(26, N'Mega GS Cao Thắng', N'Tầng 6-7, 19 Cao Thắng, Phường 2, Quận 3, TP. Hồ Chí Minh', '02862649911', 1);
SET IDENTITY_INSERT RapChieu OFF;
GO

-- =========================================================================
-- 6. BẢNG PhongChieu (28 bản ghi >= 25)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng PhongChieu...';
SET IDENTITY_INSERT PhongChieu ON;

INSERT INTO PhongChieu (MaPhong, MaRap, TenPhong, LoaiPhong, SoLuongGhe) VALUES
(1, 1, N'Phòng 01 (IMAX)', N'IMAX', 150),
(2, 1, N'Phòng 02 (2D Standard)', N'2D', 120),
(3, 2, N'Phòng 01 (ScreenX)', N'ScreenX', 130),
(4, 2, N'Phòng 02 (Gold Class)', N'VIP', 60),
(5, 3, N'Phòng IMAX Laser', N'IMAX', 200),
(6, 3, N'Phòng 02 (3D Atmos)', N'3D', 140),
(7, 4, N'Phòng Chiếu 1', N'2D', 100),
(8, 4, N'Phòng Chiếu 2', N'3D', 100),
(9, 5, N'Phòng 01 Đà Nẵng', N'2D', 120),
(10, 6, N'Phòng Superplex', N'Superplex', 180),
(11, 7, N'Phòng 01 Nam Sài Gòn', N'2D', 110),
(12, 8, N'Phòng 01 Gò Vấp', N'3D', 110),
(13, 9, N'Phòng 01 Cần Thơ', N'2D', 90),
(14, 10, N'Phòng 01 Nha Trang', N'2D', 90),
(15, 11, N'Phòng First Class', N'VIP', 50),
(16, 12, N'Phòng 01 BHD Phạm Ngọc Thạch', N'2D', 120),
(17, 13, N'Phòng 01 BHD The Garden', N'2D', 100),
(18, 14, N'Phòng 01 BHD Cầu Giấy', N'2D', 100),
(19, 15, N'Phòng 01 Galaxy Nguyễn Du', N'2D', 140),
(20, 16, N'Phòng 01 Galaxy Tràng Thi', N'2D', 110),
(21, 17, N'Phòng 01 Galaxy Tân Bình', N'2D', 120),
(22, 18, N'Phòng IMAX Laser Sala', N'IMAX', 180),
(23, 19, N'Phòng Chiếu Số 1 Quốc Gia', N'2D', 250),
(24, 20, N'Phòng 01 Cinestar Quốc Thanh', N'2D', 120),
(25, 21, N'Phòng 01 Cinestar Hai Bà Trưng', N'2D', 100),
(26, 22, N'Phòng 01 Sinh Viên', N'2D', 150),
(27, 23, N'Phòng 01 Beta Đan Phượng', N'2D', 100),
(28, 24, N'Phòng 01 Beta Thanh Xuân', N'2D', 120);
SET IDENTITY_INSERT PhongChieu OFF;
GO

-- =========================================================================
-- 7. BẢNG GheNgoi (40 bản ghi >= 25)
-- Các loại ghế: Thuong, VIP, Doi (Sweetbox)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng GheNgoi...';
SET IDENTITY_INSERT GheNgoi ON;

INSERT INTO GheNgoi (MaGhe, MaPhong, HangGhe, SoGhe, LoaiGhe, TrangThai) VALUES
(1, 1, 'A', 1, N'Thuong', 1),
(2, 1, 'A', 2, N'Thuong', 1),
(3, 1, 'A', 3, N'Thuong', 1),
(4, 1, 'A', 4, N'Thuong', 1),
(5, 1, 'A', 5, N'Thuong', 1),
(6, 1, 'A', 6, N'Thuong', 1),
(7, 1, 'A', 7, N'Thuong', 1),
(8, 1, 'A', 8, N'Thuong', 1),
(9, 1, 'B', 1, N'Thuong', 1),
(10, 1, 'B', 2, N'Thuong', 1),
(11, 1, 'B', 3, N'Thuong', 1),
(12, 1, 'B', 4, N'Thuong', 1),
(13, 1, 'B', 5, N'Thuong', 1),
(14, 1, 'B', 6, N'Thuong', 1),
(15, 1, 'B', 7, N'Thuong', 1),
(16, 1, 'B', 8, N'Thuong', 1),
(17, 1, 'C', 1, N'VIP', 1),
(18, 1, 'C', 2, N'VIP', 1),
(19, 1, 'C', 3, N'VIP', 1),
(20, 1, 'C', 4, N'VIP', 1),
(21, 1, 'C', 5, N'VIP', 1),
(22, 1, 'C', 6, N'VIP', 1),
(23, 1, 'C', 7, N'VIP', 1),
(24, 1, 'C', 8, N'VIP', 1),
(25, 1, 'D', 1, N'VIP', 1),
(26, 1, 'D', 2, N'VIP', 1),
(27, 1, 'D', 3, N'VIP', 1),
(28, 1, 'D', 4, N'VIP', 1),
(29, 1, 'D', 5, N'VIP', 1),
(30, 1, 'D', 6, N'VIP', 1),
(31, 1, 'D', 7, N'VIP', 1),
(32, 1, 'D', 8, N'VIP', 1),
(33, 1, 'E', 1, N'Doi', 1),
(34, 1, 'E', 2, N'Doi', 1),
(35, 1, 'E', 3, N'Doi', 1),
(36, 1, 'E', 4, N'Doi', 1),
(37, 2, 'A', 1, N'Thuong', 1),
(38, 2, 'A', 2, N'Thuong', 1),
(39, 2, 'A', 3, N'Thuong', 1),
(40, 2, 'A', 4, N'Thuong', 1);
SET IDENTITY_INSERT GheNgoi OFF;
GO

-- =========================================================================
-- 8. BẢNG SuatChieu (28 bản ghi >= 25)
-- Đảm bảo check constraint GioKetThuc > GioBatDau và GiaVe >= 0
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng SuatChieu...';
SET IDENTITY_INSERT SuatChieu ON;

INSERT INTO SuatChieu (MaSuatChieu, MaPhim, MaPhong, NgayChieu, GioBatDau, GioKetThuc, GiaVe, TrangThai) VALUES
(1, 11, 1, '2026-11-04', '08:30:00', '11:15:00', 120000.00, N'DangMoBan'),
(2, 11, 1, '2026-11-04', '11:45:00', '14:30:00', 120000.00, N'DangMoBan'),
(3, 13, 1, '2026-11-04', '15:00:00', '17:15:00', 120000.00, N'DangMoBan'),
(4, 13, 1, '2026-11-04', '18:00:00', '20:15:00', 140000.00, N'DangMoBan'),
(5, 23, 1, '2026-11-04', '20:45:00', '23:00:00', 150000.00, N'DangMoBan'),
(6, 14, 2, '2026-11-04', '09:00:00', '10:45:00', 85000.00, N'DangMoBan'),
(7, 15, 2, '2026-11-04', '11:15:00', '13:15:00', 85000.00, N'DangMoBan'),
(8, 16, 2, '2026-11-04', '14:00:00', '16:00:00', 85000.00, N'DangMoBan'),
(9, 21, 2, '2026-11-04', '16:30:00', '18:15:00', 95000.00, N'DangMoBan'),
(10, 24, 2, '2026-11-04', '19:00:00', '21:30:00', 100000.00, N'DangMoBan'),
(11, 22, 3, '2026-11-05', '09:30:00', '11:15:00', 90000.00, N'DangMoBan'),
(12, 11, 3, '2026-11-05', '13:00:00', '15:45:00', 110000.00, N'DangMoBan'),
(13, 23, 3, '2026-11-05', '16:30:00', '18:45:00', 120000.00, N'DangMoBan'),
(14, 24, 4, '2026-11-05', '19:30:00', '22:00:00', 180000.00, N'DangMoBan'),
(15, 13, 5, '2026-11-05', '10:00:00', '12:15:00', 150000.00, N'DangMoBan'),
(16, 11, 5, '2026-11-05', '14:00:00', '16:45:00', 160000.00, N'DangMoBan'),
(17, 23, 5, '2026-11-05', '18:30:00', '20:45:00', 180000.00, N'DangMoBan'),
(18, 14, 6, '2026-11-06', '09:00:00', '10:45:00', 95000.00, N'DangMoBan'),
(19, 15, 7, '2026-11-06', '13:30:00', '15:30:00', 80000.00, N'DangMoBan'),
(20, 16, 8, '2026-11-06', '16:00:00', '18:00:00', 85000.00, N'DangMoBan'),
(21, 21, 9, '2026-11-06', '19:00:00', '20:45:00', 90000.00, N'DangMoBan'),
(22, 24, 10, '2026-11-06', '20:30:00', '23:00:00', 110000.00, N'DangMoBan'),
(23, 11, 11, '2026-11-07', '09:00:00', '11:45:00', 85000.00, N'DangMoBan'),
(24, 13, 12, '2026-11-07', '13:00:00', '15:15:00', 90000.00, N'DangMoBan'),
(25, 23, 15, '2026-11-07', '18:00:00', '20:15:00', 200000.00, N'DangMoBan'),
(26, 24, 19, '2026-11-07', '20:30:00', '23:00:00', 90000.00, N'DangMoBan'),
(27, 26, 22, '2026-12-25', '10:00:00', '12:00:00', 160000.00, N'SapChieu'),
(28, 26, 23, '2026-12-25', '14:00:00', '16:00:00', 100000.00, N'SapChieu');
SET IDENTITY_INSERT SuatChieu OFF;
GO

-- =========================================================================
-- 9. BẢNG GheSuatChieu (32 bản ghi >= 25)
-- TrangThai: Trong, DangGiu, DaDat
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng GheSuatChieu...';
SET IDENTITY_INSERT GheSuatChieu ON;

INSERT INTO GheSuatChieu (MaGheSuatChieu, MaSuatChieu, MaGhe, GiaVe, TrangThai, ThoiGianHetHan) VALUES
(1, 1, 1, 120000.00, N'DaDat', NULL),
(2, 2, 2, 120000.00, N'DaDat', NULL),
(3, 3, 3, 120000.00, N'DaDat', NULL),
(4, 4, 4, 120000.00, N'DaDat', NULL),
(5, 5, 5, 120000.00, N'DaDat', NULL),
(6, 1, 6, 120000.00, N'DaDat', NULL),
(7, 2, 7, 120000.00, N'DaDat', NULL),
(8, 3, 8, 120000.00, N'DaDat', NULL),
(9, 4, 9, 120000.00, N'DaDat', NULL),
(10, 5, 10, 120000.00, N'DaDat', NULL),
(11, 1, 11, 120000.00, N'DaDat', NULL),
(12, 2, 12, 120000.00, N'DaDat', NULL),
(13, 3, 13, 120000.00, N'DaDat', NULL),
(14, 4, 14, 120000.00, N'DaDat', NULL),
(15, 5, 15, 120000.00, N'DaDat', NULL),
(16, 1, 16, 120000.00, N'DaDat', NULL),
(17, 2, 17, 140000.00, N'DaDat', NULL),
(18, 3, 18, 140000.00, N'DaDat', NULL),
(19, 4, 19, 140000.00, N'DaDat', NULL),
(20, 5, 20, 140000.00, N'DaDat', NULL),
(21, 1, 21, 140000.00, N'DaDat', NULL),
(22, 2, 22, 140000.00, N'DaDat', NULL),
(23, 3, 23, 140000.00, N'DaDat', NULL),
(24, 4, 24, 140000.00, N'DaDat', NULL),
(25, 5, 25, 140000.00, N'DaDat', NULL),
(26, 1, 26, 140000.00, N'DaDat', NULL),
(27, 1, 27, 140000.00, N'DangGiu', '2026-11-04 08:45:00'),
(28, 1, 28, 140000.00, N'DangGiu', '2026-11-04 08:45:00'),
(29, 2, 29, 140000.00, N'DangGiu', '2026-11-04 11:55:00'),
(30, 2, 30, 140000.00, N'DangGiu', '2026-11-04 11:55:00'),
(31, 1, 31, 140000.00, N'Trong', NULL),
(32, 1, 32, 140000.00, N'Trong', NULL);
SET IDENTITY_INSERT GheSuatChieu OFF;
GO

-- =========================================================================
-- 10. BẢNG DatVe (26 bản ghi >= 25)
-- TrangThai: ChoThanhToan, DaThanhToan, DaHuy, HetHan
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng DatVe...';
SET IDENTITY_INSERT DatVe ON;

INSERT INTO DatVe (MaDatVe, MaNguoiDung, MaSuatChieu, NgayDat, TongTien, TrangThai) VALUES
(1, 8, 1, '2026-11-02 11:17:00', 120000.00, N'DaThanhToan'),
(2, 9, 2, '2026-11-03 12:19:00', 120000.00, N'DaThanhToan'),
(3, 10, 3, '2026-11-01 13:21:00', 120000.00, N'DaThanhToan'),
(4, 11, 4, '2026-11-02 14:23:00', 120000.00, N'DaThanhToan'),
(5, 12, 5, '2026-11-03 15:25:00', 120000.00, N'DaThanhToan'),
(6, 13, 1, '2026-11-01 16:27:00', 120000.00, N'DaThanhToan'),
(7, 14, 2, '2026-11-02 17:29:00', 120000.00, N'DaThanhToan'),
(8, 15, 3, '2026-11-03 18:31:00', 120000.00, N'DaThanhToan'),
(9, 16, 4, '2026-11-01 19:33:00', 120000.00, N'DaThanhToan'),
(10, 17, 5, '2026-11-02 10:35:00', 120000.00, N'DaThanhToan'),
(11, 18, 1, '2026-11-03 11:37:00', 120000.00, N'DaThanhToan'),
(12, 19, 2, '2026-11-01 12:39:00', 120000.00, N'DaThanhToan'),
(13, 20, 3, '2026-11-02 13:41:00', 120000.00, N'DaThanhToan'),
(14, 21, 4, '2026-11-03 14:43:00', 120000.00, N'DaThanhToan'),
(15, 22, 5, '2026-11-01 15:45:00', 120000.00, N'DaThanhToan'),
(16, 23, 1, '2026-11-02 16:47:00', 120000.00, N'DaThanhToan'),
(17, 24, 2, '2026-11-03 17:49:00', 140000.00, N'DaThanhToan'),
(18, 25, 3, '2026-11-01 18:51:00', 140000.00, N'DaThanhToan'),
(19, 26, 4, '2026-11-02 19:53:00', 140000.00, N'DaThanhToan'),
(20, 27, 5, '2026-11-03 10:15:00', 140000.00, N'DaThanhToan'),
(21, 28, 1, '2026-11-01 11:17:00', 140000.00, N'DaThanhToan'),
(22, 7, 2, '2026-11-02 12:19:00', 140000.00, N'DaThanhToan'),
(23, 8, 3, '2026-11-03 13:21:00', 140000.00, N'ChoThanhToan'),
(24, 9, 4, '2026-11-01 14:23:00', 140000.00, N'ChoThanhToan'),
(25, 10, 5, '2026-11-02 15:25:00', 140000.00, N'DaHuy'),
(26, 11, 1, '2026-11-03 16:27:00', 140000.00, N'HetHan');
SET IDENTITY_INSERT DatVe OFF;
GO

-- =========================================================================
-- 11. BẢNG ChiTietDatVe (26 bản ghi >= 25)
-- UNIQUE (MaGheSuatChieu)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng ChiTietDatVe...';
SET IDENTITY_INSERT ChiTietDatVe ON;

INSERT INTO ChiTietDatVe (MaChiTiet, MaDatVe, MaGheSuatChieu, DonGia) VALUES
(1, 1, 1, 120000.00),
(2, 2, 2, 120000.00),
(3, 3, 3, 120000.00),
(4, 4, 4, 120000.00),
(5, 5, 5, 120000.00),
(6, 6, 6, 120000.00),
(7, 7, 7, 120000.00),
(8, 8, 8, 120000.00),
(9, 9, 9, 120000.00),
(10, 10, 10, 120000.00),
(11, 11, 11, 120000.00),
(12, 12, 12, 120000.00),
(13, 13, 13, 120000.00),
(14, 14, 14, 120000.00),
(15, 15, 15, 120000.00),
(16, 16, 16, 120000.00),
(17, 17, 17, 140000.00),
(18, 18, 18, 140000.00),
(19, 19, 19, 140000.00),
(20, 20, 20, 140000.00),
(21, 21, 21, 140000.00),
(22, 22, 22, 140000.00),
(23, 23, 23, 140000.00),
(24, 24, 24, 140000.00),
(25, 25, 25, 140000.00),
(26, 26, 26, 140000.00);
SET IDENTITY_INSERT ChiTietDatVe OFF;
GO

-- =========================================================================
-- 12. BẢNG ThanhToan (26 bản ghi >= 25)
-- TrangThai: ChoXuLy, ThanhCong, ThatBai, HoanTien
-- PhuongThuc: MoMo, VNPay, ZaloPay, TheTinDung, ShopeePay
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng ThanhToan...';
SET IDENTITY_INSERT ThanhToan ON;

INSERT INTO ThanhToan (MaThanhToan, MaDatVe, PhuongThuc, MaGiaoDich, SoTien, NgayThanhToan, TrangThai) VALUES
(1, 1, N'VNPay', 'TXN2026110200001', 120000.00, '2026-11-02 11:17:00', N'ThanhCong'),
(2, 2, N'ZaloPay', 'TXN2026110300002', 120000.00, '2026-11-03 12:19:00', N'ThanhCong'),
(3, 3, N'TheTinDung', 'TXN2026110100003', 120000.00, '2026-11-01 13:21:00', N'ThanhCong'),
(4, 4, N'ShopeePay', 'TXN2026110200004', 120000.00, '2026-11-02 14:23:00', N'ThanhCong'),
(5, 5, N'MoMo', 'TXN2026110300005', 120000.00, '2026-11-03 15:25:00', N'ThanhCong'),
(6, 6, N'VNPay', 'TXN2026110100006', 120000.00, '2026-11-01 16:27:00', N'ThanhCong'),
(7, 7, N'ZaloPay', 'TXN2026110200007', 120000.00, '2026-11-02 17:29:00', N'ThanhCong'),
(8, 8, N'TheTinDung', 'TXN2026110300008', 120000.00, '2026-11-03 18:31:00', N'ThanhCong'),
(9, 9, N'ShopeePay', 'TXN2026110100009', 120000.00, '2026-11-01 19:33:00', N'ThanhCong'),
(10, 10, N'MoMo', 'TXN2026110200010', 120000.00, '2026-11-02 10:35:00', N'ThanhCong'),
(11, 11, N'VNPay', 'TXN2026110300011', 120000.00, '2026-11-03 11:37:00', N'ThanhCong'),
(12, 12, N'ZaloPay', 'TXN2026110100012', 120000.00, '2026-11-01 12:39:00', N'ThanhCong'),
(13, 13, N'TheTinDung', 'TXN2026110200013', 120000.00, '2026-11-02 13:41:00', N'ThanhCong'),
(14, 14, N'ShopeePay', 'TXN2026110300014', 120000.00, '2026-11-03 14:43:00', N'ThanhCong'),
(15, 15, N'MoMo', 'TXN2026110100015', 120000.00, '2026-11-01 15:45:00', N'ThanhCong'),
(16, 16, N'VNPay', 'TXN2026110200016', 120000.00, '2026-11-02 16:47:00', N'ThanhCong'),
(17, 17, N'ZaloPay', 'TXN2026110300017', 140000.00, '2026-11-03 17:49:00', N'ThanhCong'),
(18, 18, N'TheTinDung', 'TXN2026110100018', 140000.00, '2026-11-01 18:51:00', N'ThanhCong'),
(19, 19, N'ShopeePay', 'TXN2026110200019', 140000.00, '2026-11-02 19:53:00', N'ThanhCong'),
(20, 20, N'MoMo', 'TXN2026110300020', 140000.00, '2026-11-03 10:15:00', N'ThanhCong'),
(21, 21, N'VNPay', 'TXN2026110100021', 140000.00, '2026-11-01 11:17:00', N'ThanhCong'),
(22, 22, N'ZaloPay', 'TXN2026110200022', 140000.00, '2026-11-02 12:19:00', N'ThanhCong'),
(23, 23, N'TheTinDung', 'TXN2026110300023', 140000.00, '2026-11-03 13:21:00', N'ChoXuLy'),
(24, 24, N'ShopeePay', 'TXN2026110100024', 140000.00, '2026-11-01 14:23:00', N'ChoXuLy'),
(25, 25, N'MoMo', 'TXN2026110200025', 140000.00, '2026-11-02 15:25:00', N'HoanTien'),
(26, 26, N'VNPay', 'TXN2026110300026', 140000.00, '2026-11-03 16:27:00', N'ThatBai');
SET IDENTITY_INSERT ThanhToan OFF;
GO

-- =========================================================================
-- 13. BẢNG VeDienTu (26 bản ghi >= 25)
-- TrangThai: ChuaSuDung, DaSuDung, DaHuy
-- UNIQUE (MaChiTiet), UNIQUE (MaQR)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng VeDienTu...';
SET IDENTITY_INSERT VeDienTu ON;

INSERT INTO VeDienTu (MaVe, MaChiTiet, MaQR, NgayPhatHanh, TrangThai, NgaySuDung) VALUES
(1, 1, 'QR-CSDL-2026-TICKET-0001-1001', '2026-11-02 11:17:00', N'DaSuDung', '2026-11-04 18:00:00'),
(2, 2, 'QR-CSDL-2026-TICKET-0002-1002', '2026-11-03 12:19:00', N'DaSuDung', '2026-11-04 18:00:00'),
(3, 3, 'QR-CSDL-2026-TICKET-0003-1003', '2026-11-01 13:21:00', N'DaSuDung', '2026-11-04 18:00:00'),
(4, 4, 'QR-CSDL-2026-TICKET-0004-1004', '2026-11-02 14:23:00', N'DaSuDung', '2026-11-04 18:00:00'),
(5, 5, 'QR-CSDL-2026-TICKET-0005-1005', '2026-11-03 15:25:00', N'DaSuDung', '2026-11-04 18:00:00'),
(6, 6, 'QR-CSDL-2026-TICKET-0006-1006', '2026-11-01 16:27:00', N'DaSuDung', '2026-11-04 18:00:00'),
(7, 7, 'QR-CSDL-2026-TICKET-0007-1007', '2026-11-02 17:29:00', N'DaSuDung', '2026-11-04 18:00:00'),
(8, 8, 'QR-CSDL-2026-TICKET-0008-1008', '2026-11-03 18:31:00', N'DaSuDung', '2026-11-04 18:00:00'),
(9, 9, 'QR-CSDL-2026-TICKET-0009-1009', '2026-11-01 19:33:00', N'DaSuDung', '2026-11-04 18:00:00'),
(10, 10, 'QR-CSDL-2026-TICKET-0010-1010', '2026-11-02 10:35:00', N'DaSuDung', '2026-11-04 18:00:00'),
(11, 11, 'QR-CSDL-2026-TICKET-0011-1011', '2026-11-03 11:37:00', N'ChuaSuDung', NULL),
(12, 12, 'QR-CSDL-2026-TICKET-0012-1012', '2026-11-01 12:39:00', N'ChuaSuDung', NULL),
(13, 13, 'QR-CSDL-2026-TICKET-0013-1013', '2026-11-02 13:41:00', N'ChuaSuDung', NULL),
(14, 14, 'QR-CSDL-2026-TICKET-0014-1014', '2026-11-03 14:43:00', N'ChuaSuDung', NULL),
(15, 15, 'QR-CSDL-2026-TICKET-0015-1015', '2026-11-01 15:45:00', N'ChuaSuDung', NULL),
(16, 16, 'QR-CSDL-2026-TICKET-0016-1016', '2026-11-02 16:47:00', N'ChuaSuDung', NULL),
(17, 17, 'QR-CSDL-2026-TICKET-0017-1017', '2026-11-03 17:49:00', N'ChuaSuDung', NULL),
(18, 18, 'QR-CSDL-2026-TICKET-0018-1018', '2026-11-01 18:51:00', N'ChuaSuDung', NULL),
(19, 19, 'QR-CSDL-2026-TICKET-0019-1019', '2026-11-02 19:53:00', N'ChuaSuDung', NULL),
(20, 20, 'QR-CSDL-2026-TICKET-0020-1020', '2026-11-03 10:15:00', N'ChuaSuDung', NULL),
(21, 21, 'QR-CSDL-2026-TICKET-0021-1021', '2026-11-01 11:17:00', N'ChuaSuDung', NULL),
(22, 22, 'QR-CSDL-2026-TICKET-0022-1022', '2026-11-02 12:19:00', N'ChuaSuDung', NULL),
(23, 23, 'QR-CSDL-2026-TICKET-0023-1023', '2026-11-03 13:21:00', N'ChuaSuDung', NULL),
(24, 24, 'QR-CSDL-2026-TICKET-0024-1024', '2026-11-01 14:23:00', N'ChuaSuDung', NULL),
(25, 25, 'QR-CSDL-2026-TICKET-0025-1025', '2026-11-02 15:25:00', N'DaHuy', NULL),
(26, 26, 'QR-CSDL-2026-TICKET-0026-1026', '2026-11-03 16:27:00', N'ChuaSuDung', NULL);
SET IDENTITY_INSERT VeDienTu OFF;
GO

-- =========================================================================
-- 14. BẢNG ThongBao (26 bản ghi >= 25)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng ThongBao...';
SET IDENTITY_INSERT ThongBao ON;

INSERT INTO ThongBao (MaThongBao, MaNguoiDung, TieuDe, NoiDung, NgayGui, DaDoc) VALUES
(1, 1, N'Chào mừng Quản trị viên', N'Hệ thống quản lý cụm rạp chiếu phim đã sẵn sàng đi vào vận hành.', '2024-01-01 08:30:00', 1),
(2, 2, N'Cập nhật chính sách giá vé', N'Chính sách giá vé áp dụng cho các suất chiếu cuối tuần đã được cập nhật.', '2024-01-02 10:00:00', 1),
(3, 3, N'Phân ca trực phòng vé', N'Bạn đã được phân công ca trực sáng thứ 7 tại rạp CGV Vincom Bà Triệu.', '2024-01-06 08:00:00', 1),
(4, 4, N'Hướng dẫn quét mã vé QR', N'Vui lòng xem tài liệu hướng dẫn mới về việc xác thực mã QR vé điện tử trên app nhân viên.', '2024-01-06 09:00:00', 1),
(5, 7, N'Đặt vé thành công', N'Bạn đã thanh toán thành công vé xem phim Mai. Mã vé QR đã được tạo.', '2026-11-01 10:20:00', 1),
(6, 8, N'Đặt vé thành công', N'Bạn đã thanh toán thành công vé xem phim Lật Mặt 7. Vui lòng xuất trình mã QR tại quầy soát vé.', '2026-11-01 11:30:00', 1),
(7, 9, N'Nhắc nhở suất chiếu sắp tới', N'Suất chiếu phim Dune 2 của bạn sẽ bắt đầu trong 60 phút nữa.', '2026-11-02 07:30:00', 1),
(8, 10, N'Ưu đãi thành viên CGV Member', N'Tặng bạn voucher giảm 20% combo bỏng nước cho lần đặt vé kế tiếp.', '2026-11-02 09:00:00', 0),
(9, 11, N'Xác nhận hoàn tiền', N'Yêu cầu hoàn tiền vé xem phim của bạn đã được xử lý thành công qua MoMo.', '2026-11-02 14:00:00', 1),
(10, 12, N'Vé điện tử của bạn đã sẵn sàng', N'Vé xem phim Godzilla x Kong tại phòng chiếu IMAX đã sẵn sàng trong mục Vé của tôi.', '2026-11-02 16:00:00', 0),
(11, 13, N'Cảnh báo hết hạn giữ ghế', N'Suất chiếu của bạn chỉ còn 5 phút để hoàn tất thanh toán trước khi ghế bị hủy.', '2026-11-03 08:40:00', 1),
(12, 14, N'Đánh giá phim sau khi xem', N'Bạn thấy bộ phim Kung Fu Panda 4 thế nào? Hãy chia sẻ đánh giá để nhận 50 điểm tích lũy.', '2026-11-03 10:15:00', 0),
(13, 15, N'Thanh toán thành công', N'Giao dịch VNPay số tiền 120,000đ cho mã đặt vé #15 đã thành công.', '2026-11-03 11:00:00', 1),
(14, 16, N'Phim mới sắp chiếu', N'Bom tấn Mufasa: Vua Sư Tử chính thức mở bán vé sớm. Đặt ngay để có chỗ ngồi ưng ý!', '2026-11-03 14:00:00', 0),
(15, 17, N'Đặt vé thành công', N'Bạn đã đặt thành công ghế C-01 tại CGV Vincom Bà Triệu.', '2026-11-03 15:30:00', 0),
(16, 18, N'Cập nhật ứng dụng phiên bản mới', N'Tính năng chọn combo bắp nước trực tiếp khi đặt vé đã có mặt trên phiên bản mới.', '2026-11-03 16:00:00', 0),
(17, 19, N'Chúc mừng sinh nhật thành viên', N'Tặng bạn 01 vé xem phim 2D miễn phí nhân dịp sinh nhật trong tháng này!', '2026-11-03 16:30:00', 1),
(18, 20, N'Xác nhận đặt vé #20', N'Vé điện tử xem phim Đào, Phở và Piano đã được gửi về email của bạn.', '2026-11-03 17:00:00', 0),
(19, 21, N'Thông báo bảo trì hệ thống', N'Hệ thống thanh toán qua ZaloPay sẽ tạm dừng nâng cấp từ 01:00 đến 03:00 ngày mai.', '2026-11-03 17:15:00', 0),
(20, 22, N'Ưu đãi giá vé thứ 4 vui vẻ', N'Đồng giá vé chỉ từ 50k cho tất cả các suất chiếu 2D vào thứ 4 hàng tuần.', '2026-11-03 17:20:00', 0),
(21, 23, N'Thanh toán thất bại', N'Giao dịch thanh toán vé #24 không thành công do thẻ hết hạn mức. Vui lòng thử lại.', '2026-11-03 17:25:00', 1),
(22, 24, N'Vé đã sử dụng', N'Mã QR vé xem phim #1 đã được nhân viên kiểm soát vé quét xác thực thành công.', '2026-11-03 17:30:00', 1),
(23, 25, N'Vé của bạn bị hủy do quá hạn', N'Đơn đặt vé #26 đã tự động bị hủy do không thanh toán trong thời gian quy định.', '2026-11-03 17:35:00', 1),
(24, 26, N'Đánh giá nhận voucher', N'Cảm ơn bạn đã đóng góp đánh giá cho phim Quật Mộ Trùng Ma (Exhuma).', '2026-11-03 17:40:00', 0),
(25, 27, N'Chào mừng thành viên mới', N'Tài khoản của bạn đã được đăng ký thành công trên Hệ Thống Đặt Vé Xem Phim.', '2026-11-03 17:45:00', 1),
(26, 28, N'Đăng nhập từ thiết bị mới', N'Phát hiện đăng nhập tài khoản của bạn trên trình duyệt Chrome lúc 17:00.', '2026-11-03 17:50:00', 0);
SET IDENTITY_INSERT ThongBao OFF;
GO

-- =========================================================================
-- 15. BẢNG DanhGiaPhim (26 bản ghi >= 25)
-- UNIQUE (MaNguoiDung, MaPhim), CHECK SoSao BETWEEN 1 AND 5
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng DanhGiaPhim...';
SET IDENTITY_INSERT DanhGiaPhim ON;

INSERT INTO DanhGiaPhim (MaDanhGia, MaNguoiDung, MaPhim, SoSao, BinhLuan, NgayDanhGia) VALUES
(1, 7, 1, 5, N'Phim rất xúc động, diễn xuất của Phương Anh Đào và Tuấn Trần quá đỉnh cao. Kịch bản chạm tới trái tim người xem.', '2024-02-15 14:00:00'),
(2, 8, 2, 5, N'Lý Hải làm phim gia đình cảm động rơi nước mắt. Cảnh mẹ già đợi các con quá thương tâm.', '2024-05-01 16:30:00'),
(3, 9, 3, 4, N'Hình ảnh Hà Nội xưa hào hùng và lãng mạn. Âm nhạc và góc quay rất nghệ thuật.', '2024-02-20 20:15:00'),
(4, 10, 4, 4, N'Phim phản ánh đúng thực tế gia đình Việt, lời thoại chân thật nhưng hơi nhiều tiếng chửi một chút.', '2023-01-25 18:00:00'),
(5, 11, 5, 5, N'Xem khóc sưng cả mắt, tình cảm cha con được thể hiện vô cùng mộc mạc và chân thành.', '2021-03-10 19:45:00'),
(6, 12, 6, 5, N'Nhạc phim Phan Mạnh Quỳnh quá tuyệt vời. Diễn viên Ngạn và Hà Lan như bước ra từ trang sách.', '2020-01-05 21:00:00'),
(7, 13, 7, 4, N'Hành động mãn nhãn, đẳng cấp quốc tế của Ngô Thanh Vân. Đánh đấm rất có lực và nghẹt thở.', '2019-03-01 22:15:00'),
(8, 14, 8, 4, N'Phim hài hước trẻ trung, giải trí rất tốt. Kiều Minh Tuấn và Kaity Nguyễn diễn xuất ăn ý bất ngờ.', '2017-05-05 15:30:00'),
(9, 15, 9, 3, N'Ý tưởng văn hóa dân gian hay, không khí rùng rợn nhưng kỹ xảo CGI còn hơi giả.', '2024-01-10 17:20:00'),
(10, 16, 10, 4, N'Bối cảnh Làng Địa Ngục dựng rất kỳ công và rùng rợn. Phục trang truyền thống đẹp mắt.', '2023-12-20 18:40:00'),
(11, 17, 11, 5, N'Một kiệt tác điện ảnh khoa học viễn tưởng thế kỷ 21! Hình ảnh và âm thanh của Hans Zimmer quá choáng ngợp trên màn IMAX.', '2024-03-05 20:00:00'),
(12, 18, 12, 5, N'Christopher Nolan không bao giờ làm khán giả thất vọng. Cillian Murphy xứng đáng với giải Oscar.', '2023-08-15 16:00:00'),
(13, 19, 13, 4, N'Hành động đã mắt, giải trí đỉnh cao. Đánh nhau từ đầu đến cuối xem cực kỳ phấn khích.', '2024-04-02 14:15:00'),
(14, 20, 14, 4, N'Po vẫn giữ được nét đáng yêu quen thuộc. Đồ họa rực rỡ, các pha võ thuật kungfu đẹp mắt.', '2024-03-12 11:30:00'),
(15, 21, 15, 5, N'Phần Conan hay nhất trong vài năm trở lại đây. Cặp đôi Kaito Kid và Heiji tương tác cực kỳ thú vị.', '2024-08-05 19:10:00'),
(16, 22, 16, 5, N'Âm nhạc lay động tâm hồn, thông điệp bảo vệ thế giới và sức mạnh của âm nhạc rất nhân văn.', '2024-05-28 15:00:00'),
(17, 23, 17, 5, N'Hình ảnh đại dương Pandora đẹp đến nghẹt thở. Trải nghiệm 3D tuyệt đỉnh không đâu sánh bằng.', '2022-12-20 18:30:00'),
(18, 24, 18, 5, N'Khoảnh khắc cả 3 người nhện cùng xuất hiện làm cả rạp chiếu vỗ tay bùng nổ. Cảm xúc trọn vẹn!', '2021-12-22 21:40:00'),
(19, 25, 19, 5, N'Hồi kết vĩ đại cho hành trình 10 năm MCU. I love you 3000 Tony Stark!', '2019-05-02 20:30:00'),
(20, 26, 20, 5, N'Bộ phim hay nhất mọi thời đại về vũ trụ và tình cha con. Cảnh quay hố đen Gargantua quá vĩ đại.', '2014-11-15 19:00:00'),
(21, 27, 21, 5, N'Các nhân vật cảm xúc mới như Lo Âu (Anxiety) được xây dựng quá xuất sắc, đánh trúng tâm lý tuổi trẻ.', '2024-06-20 17:45:00'),
(22, 28, 22, 4, N'Hài hước dí dỏm, lũ Minions siêu quậy cười đau cả bụng. Rất thích hợp đi xem cùng gia đình.', '2024-07-10 14:00:00'),
(23, 7, 23, 5, N'Màn kết hợp bùng nổ giữa Deadpool và Wolverine, ngập tràn các vai cameo bất ngờ và những pha chém giết hành động đỉnh chóp.', '2024-07-30 22:00:00'),
(24, 8, 24, 5, N'Phim kinh dị phong thủy của Hàn Quốc cực kỳ lôi cuốn, dàn diễn viên gạo cội nhập vai xuất thần.', '2024-03-20 18:15:00'),
(25, 9, 25, 4, N'Phim ấm áp, tái hiện lại bối cảnh Sài Gòn thập niên 90 rất thơ và hoài niệm.', '2024-02-18 16:20:00'),
(26, 10, 26, 5, N'Rất hóng chờ phim Mufasa ra mắt, teaser trailer quá ấn tượng!', '2024-11-01 10:00:00');
SET IDENTITY_INSERT DanhGiaPhim OFF;
GO

PRINT N'=========================================================================';
PRINT N'===> ĐÃ CHÈN TOÀN BỘ DỮ LIỆU MẪU CHO 15 BẢNG THÀNH CÔNG! (Mỗi bảng >= 25 dòng)';
PRINT N'=========================================================================';
GO

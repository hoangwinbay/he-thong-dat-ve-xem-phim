# -*- coding: utf-8 -*-
import sys
import os

sys.stdout.reconfigure(encoding='utf-8')

# Chuẩn bị dữ liệu cho 15 bảng

# 1. NguoiDung (28 rows)
users = [
    (1, "Nguyễn Văn An", "nguyenvanan.admin@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0901234567", "QuanTri", "2024-01-01 08:00:00", 1),
    (2, "Trần Thị Mai", "tranthimai.admin@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0902345678", "QuanTri", "2024-01-02 09:30:00", 1),
    (3, "Lê Hoàng Long", "lehoanglong.staff@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0912345671", "NhanVien", "2024-01-05 10:00:00", 1),
    (4, "Phạm Minh Thảo", "phamminhthao.staff@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0912345672", "NhanVien", "2024-01-05 10:15:00", 1),
    (5, "Đỗ Đức Thịnh", "doducthinh.staff@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0912345673", "NhanVien", "2024-01-06 14:00:00", 1),
    (6, "Vũ Thúy Kiều", "vuthuykieu.staff@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0912345674", "NhanVien", "2024-01-06 15:30:00", 1),
    (7, "Bùi Tuấn Anh", "buituananh@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0981112233", "KhachHang", "2024-01-10 09:00:00", 1),
    (8, "Hoàng Ngọc Ánh", "hoangngocanh@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0982223344", "KhachHang", "2024-01-11 11:20:00", 1),
    (9, "Ngô Gia Bảo", "ngogiabao@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0983334455", "KhachHang", "2024-01-12 14:10:00", 1),
    (10, "Dương Thùy Chi", "duongthuychi@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0984445566", "KhachHang", "2024-01-13 16:45:00", 1),
    (11, "Đinh Trọng Dũng", "dinhtrongdung@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0985556677", "KhachHang", "2024-01-14 18:30:00", 1),
    (12, "Trịnh Mỹ Duyên", "trinhmyduyen@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0986667788", "KhachHang", "2024-01-15 20:00:00", 1),
    (13, "Lý Hải Đăng", "lyhaidang@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0987778899", "KhachHang", "2024-01-16 08:15:00", 1),
    (14, "Mai Phương Hoa", "maiphuonghoa@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0988889900", "KhachHang", "2024-01-17 10:40:00", 1),
    (15, "Phan Thanh Hùng", "phanthanhhung@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0989990011", "KhachHang", "2024-01-18 13:50:00", 1),
    (16, "Võ Quốc Huy", "voquochuy@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0971112244", "KhachHang", "2024-01-19 15:25:00", 1),
    (17, "Cao Thị Kim Khuyên", "caothikimkhuyen@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0972223355", "KhachHang", "2024-01-20 17:10:00", 1),
    (18, "Hồ Quang Lâm", "hoquanglam@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0973334466", "KhachHang", "2024-01-21 19:35:00", 1),
    (19, "Nguyễn Khánh Linh", "nguyenkhanhlinh@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0974445577", "KhachHang", "2024-01-22 21:00:00", 1),
    (20, "Trần Công Minh", "trancongminh@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0975556688", "KhachHang", "2024-01-23 07:45:00", 1),
    (21, "Đặng Bảo Ngọc", "dangbaongoc@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0976667799", "KhachHang", "2024-01-24 09:15:00", 1),
    (22, "Hà Minh Nhật", "haminhnhat@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0977778800", "KhachHang", "2024-01-25 11:30:00", 1),
    (23, "Lâm Vĩnh Phát", "lamvinhphat@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0978889911", "KhachHang", "2024-01-26 14:20:00", 1),
    (24, "Quách Như Quỳnh", "quachnhuquynh@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0979990022", "KhachHang", "2024-01-27 16:50:00", 1),
    (25, "Tạ Đình Sang", "tadinhsang@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0961112255", "KhachHang", "2024-01-28 18:40:00", 1),
    (26, "Uông Thanh Tâm", "uongthanhtam@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0962223366", "KhachHang", "2024-01-29 20:10:00", 1),
    (27, "Vương Anh Uyên", "vuonganhuyen@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0963334477", "KhachHang", "2024-01-30 08:30:00", 1),
    (28, "Đoàn Tiến Dũng", "doantiendung@gmail.com", "$2a$12$K8yR2u8mGqO29jB8yZfF..e1/rG0jXwz4nQ8vJ4gIe1L1fI9mY2Wa", "0964445588", "KhachHang", "2024-01-31 10:00:00", 1)
]

# 2. TheLoai (26 rows)
genres = [
    (1, "Hành Động"),
    (2, "Phiêu Lưu"),
    (3, "Hoạt Hình"),
    (4, "Hài Hước"),
    (5, "Tội Phạm"),
    (6, "Tài Liệu"),
    (7, "Chính Kịch"),
    (8, "Gia Đình"),
    (9, "Giả Tưởng"),
    (10, "Lịch Sử"),
    (11, "Kinh Dị"),
    (12, "Âm Nhạc"),
    (13, "Bí Ẩn"),
    (14, "Lãng Mạn"),
    (15, "Khoa Học Viễn Tưởng"),
    (16, "Thể Thao"),
    (17, "Giật Gân"),
    (18, "Chiến Tranh"),
    (19, "Miền Tây"),
    (20, "Võ Thuật"),
    (21, "Tâm Lý"),
    (22, "Siêu Anh Hùng"),
    (23, "Thần Thoại"),
    (24, "Trinh Thám"),
    (25, "Cổ Trang"),
    (26, "Thảm Họa")
]

# 3. Phim (26 rows)
movies = [
    (1, "Mai", "Câu chuyện xoay quanh Mai, một phụ nữ gần 40 tuổi làm nghề massage, phải đối mặt với định kiến xã hội và tìm kiếm hạnh phúc đích thực.", "Trấn Thành", "Phương Anh Đào, Tuấn Trần, Trấn Thành, Hồng Đào", 131, "2024-02-10", "2024-04-10", "Việt Nam", "T18", "Ảnh/poster_mai.jpg", "https://www.youtube.com/watch?v=mai_trailer", "NgungChieu"),
    (2, "Lật Mặt 7: Một Điều Ước", "Khắc họa tình cảm gia đình cảm động của người mẹ già cùng 5 người con trưởng thành với những hoàn cảnh và số phận khác nhau.", "Lý Hải", "Thanh Hiền, Trương Minh Cường, Đinh Y Nhung, Quách Ngọc Tuyên", 138, "2024-04-26", "2024-06-30", "Việt Nam", "K", "Ảnh/poster_lat_mat_7.jpg", "https://www.youtube.com/watch?v=latmat7_trailer", "NgungChieu"),
    (3, "Đào, Phở và Piano", "Bức tranh bi tráng và lãng mạn về tình yêu thời chiến trong bối cảnh cuộc chiến đấu bảo vệ Hà Nội mùa đông năm 1946.", "Phi Tiến Sơn", "Doãn Quốc Đam, Cao Thị Thùy Linh, Trần Lực, Tuấn Hưng", 100, "2024-02-10", "2024-04-15", "Việt Nam", "T13", "Ảnh/poster_dao_pho_piano.jpg", "https://www.youtube.com/watch?v=daophopiano_trailer", "NgungChieu"),
    (4, "Nhà Bà Nữ", "Mâu thuẫn thế hệ và áp lực gia đình trong một tiệm bánh canh cua nổi tiếng tại Sài Gòn do một người mẹ độc đoán làm chủ.", "Trấn Thành", "Lê Giang, Uyển Ân, Song Luân, Trấn Thành, Khả Như", 102, "2023-01-22", "2023-03-30", "Việt Nam", "T16", "Ảnh/poster_nha_ba_nu.jpg", "https://www.youtube.com/watch?v=nhabanu_trailer", "NgungChieu"),
    (5, "Bố Già", "Câu chuyện tình cha con đầy xúc động của Ba Sang - người đàn ông luôn lo chuyện bao đồng và con trai Quắn - YouTuber hiện đại.", "Vũ Ngọc Đãng, Trấn Thành", "Trấn Thành, Tuấn Trần, Ngân Chi, NSND Ngọc Giàu", 128, "2021-03-05", "2021-05-15", "Việt Nam", "P", "Ảnh/poster_bo_gia.jpg", "https://www.youtube.com/watch?v=bogia_trailer", "NgungChieu"),
    (6, "Mắt Biếc", "Mối tình đơn phương da diết của Ngạn dành cho cô bạn thân Hà Lan với đôi mắt biếc biếc qua bao thăng trầm tuổi thanh xuân.", "Victor Vũ", "Trần Nghĩa, Trúc Anh, Trần Phong, Đỗ Khánh Vân", 117, "2019-12-20", "2020-02-28", "Việt Nam", "P", "Ảnh/poster_mat_biec.jpg", "https://www.youtube.com/watch?v=matbiec_trailer", "NgungChieu"),
    (7, "Hai Phượng", "Hành trình nghẹt thở của một nữ giang hồ quy ẩn truy đuổi băng nhóm buôn bán trẻ em để giải cứu con gái bị bắt cóc.", "Lê Văn Kiệt", "Ngô Thanh Vân, Cát Vy, Phan Thanh Nhiên, Phạm Anh Khoa", 98, "2019-02-22", "2019-04-30", "Việt Nam", "T18", "Ảnh/poster_hai_phuong.jpg", "https://www.youtube.com/watch?v=haiphuong_trailer", "NgungChieu"),
    (8, "Em Chưa 18", "Câu chuyện tình yêu hài hước, dở khóc dở cười giữa tay chơi Hoàng hào hoa và cô nàng học sinh cá tính Linh Đan.", "Lê Thanh Sơn", "Kaity Nguyễn, Kiều Minh Tuấn, Will, Châu Bùi", 95, "2017-04-28", "2017-06-30", "Việt Nam", "T16", "Ảnh/poster_em_chua_18.jpg", "https://www.youtube.com/watch?v=emchua18_trailer", "NgungChieu"),
    (9, "Quỷ Cẩu", "Lấy cảm hứng từ truyền thuyết linh dị dân gian Chó đội nón mê, kể về những tai ương giáng xuống một gia đình làm nghề mổ chó.", "Lưu Thành Luân", "Quang Tuấn, Nam Thư, Kim Xuân, Vân Dung, Quốc Quân", 108, "2023-12-29", "2024-03-01", "Việt Nam", "T18", "Ảnh/poster_quy_cau.jpg", "https://www.youtube.com/watch?v=quycau_trailer", "NgungChieu"),
    (10, "Kẻ Ăn Hồn", "Cổ thuật Rượu Sọ Người reo rắc kinh hoàng lên Làng Địa Ngục cổ xưa, hé lộ nguồn gốc lời nguyền gia tộc đẫm máu.", "Trần Hữu Tấn", "Hoàng Hà, Võ Điền Gia Huy, Huỳnh Thanh Trực, Lan Phương", 109, "2023-12-15", "2024-02-15", "Việt Nam", "T18", "Ảnh/poster_ke_an_hon.jpg", "https://www.youtube.com/watch?v=keanhon_trailer", "NgungChieu"),
    (11, "Dune: Hành Tinh Cát 2", "Paul Atreides hợp lực cùng Chani và tộc người Fremen để trả thù những kẻ đã hủy hoại gia đình mình và định đoạt số phận vũ trụ.", "Denis Villeneuve", "Timothée Chalamet, Zendaya, Rebecca Ferguson, Javier Bardem", 166, "2024-03-01", "2024-05-15", "Mỹ", "T16", "Ảnh/poster_dune_2.jpg", "https://www.youtube.com/watch?v=dune2_trailer", "DangChieu"),
    (12, "Oppenheimer", "Tác phẩm tiểu sử lịch sử khắc họa cuộc đời của J. Robert Oppenheimer, cha đẻ của bom nguyên tử trong Dự án Manhattan.", "Christopher Nolan", "Cillian Murphy, Emily Blunt, Matt Damon, Robert Downey Jr.", 180, "2023-08-11", "2023-11-10", "Mỹ", "T18", "Ảnh/poster_oppenheimer.jpg", "https://www.youtube.com/watch?v=oppenheimer_trailer", "NgungChieu"),
    (13, "Godzilla x Kong: Đế Chế Mới", "Hai quái thú khổng lồ Godzilla và Kong phải hợp sức đối đầu với một mối đe dọa sinh tử chưa từng được biết đến từ Trái Đất Rỗng.", "Adam Wingard", "Rebecca Hall, Brian Tyree Henry, Dan Stevens, Kaylee Hottle", 115, "2024-03-29", "2024-05-30", "Mỹ", "T13", "Ảnh/poster_godzilla_kong.jpg", "https://www.youtube.com/watch?v=godzillakong_trailer", "DangChieu"),
    (14, "Kung Fu Panda 4", "Po chuẩn bị trở thành Thủ lĩnh Tinh thần của Thung lũng Bình Yên và phải tìm kiếm một Thần Long Đại Hiệp mới để kế vị.", "Mike Mitchell", "Jack Black, Awkwafina, Viola Davis, Dustin Hoffman", 94, "2024-03-08", "2024-05-20", "Mỹ", "P", "Ảnh/poster_kungfu_panda_4.jpg", "https://www.youtube.com/watch?v=kungfupanda4_trailer", "DangChieu"),
    (15, "Thám Tử Lừng Danh Conan: Ngôi Sao 5 Cánh 1 Triệu Đô", "Conan, Heiji Hattori và Siêu trộm Kaito Kid bước vào cuộc truy tìm kho báu bí ẩn liên quan đến thanh kiếm Nhật cổ tại Hakodate.", "Chika Nagaoka", "Minami Takayama, Wakana Yamazaki, Kappei Yamaguchi, Ryo Horikawa", 111, "2024-08-02", "2024-10-15", "Nhật Bản", "K", "Ảnh/poster_conan_ngoi_sao.jpg", "https://www.youtube.com/watch?v=conan_trailer", "DangChieu"),
    (16, "Doraemon: Bản Giao Hưởng Địa Cầu", "Doraemon, Nobita và nhóm bạn bắt đầu chuyến phiêu lưu âm nhạc kỳ thú để giải cứu Trái Đất khỏi một sinh vật đe dọa nuốt chửng âm thanh.", "Kazuaki Imai", "Wasabi Mizuta, Megumi Ohara, Yumi Kakazu, Subaru Kimura", 115, "2024-05-24", "2024-07-31", "Nhật Bản", "P", "Ảnh/poster_doraemon_giao_huong.jpg", "https://www.youtube.com/watch?v=doraemon_trailer", "DangChieu"),
    (17, "Avatar: Dòng Chảy Của Nước", "Gia đình Jake Sully và Neytiri phải rời khỏi khu rừng thân thuộc và hòa nhập với bộ tộc Metkayina vùng đại dương trên hành tinh Pandora.", "James Cameron", "Sam Worthington, Zoe Saldana, Sigourney Weaver, Stephen Lang", 192, "2022-12-16", "2023-03-15", "Mỹ", "T13", "Ảnh/poster_avatar_2.jpg", "https://www.youtube.com/watch?v=avatar2_trailer", "NgungChieu"),
    (18, "Spider-Man: Không Còn Nhà", "Danh tính Peter Parker bị lộ dẫn đến việc cậu nhờ Doctor Strange can thiệp ma thuật, vô tình mở ra hiểm họa đa vũ trụ.", "Jon Watts", "Tom Holland, Zendaya, Benedict Cumberbatch, Willem Dafoe", 148, "2021-12-17", "2022-03-15", "Mỹ", "T13", "Ảnh/poster_spiderman_no_way_home.jpg", "https://www.youtube.com/watch?v=spiderman_trailer", "NgungChieu"),
    (19, "Avengers: Hồi Kết", "Sau cái búng tay hủy diệt của Thanos, các Avengers sống sót tập hợp lại để thực hiện canh bạc du hành thời gian nhằm cứu vũ trụ.", "Anthony Russo, Joe Russo", "Robert Downey Jr., Chris Evans, Mark Ruffalo, Chris Hemsworth", 181, "2019-04-26", "2019-07-15", "Mỹ", "T13", "Ảnh/poster_avengers_endgame.jpg", "https://www.youtube.com/watch?v=endgame_trailer", "NgungChieu"),
    (20, "Hố Đen Tử Thần", "Nhóm nhà khoa học dẫn đầu bởi Cooper thực hiện chuyến du hành vượt ra khỏi dải ngân hà để tìm kiếm hành tinh sống mới cho nhân loại.", "Christopher Nolan", "Matthew McConaughey, Anne Hathaway, Jessica Chastain, Michael Caine", 169, "2014-11-07", "2015-02-15", "Mỹ", "T13", "Ảnh/poster_interstellar.jpg", "https://www.youtube.com/watch?v=interstellar_trailer", "NgungChieu"),
    (21, "Những Mảnh Ghép Cảm Xúc 2", "Riley bước vào tuổi dậy thì với những cảm xúc mới xuất hiện tại trung tâm điều khiển: Lo Âu, Ganh Tị, Xấu Hổ và Chán Nản.", "Kelsey Mann", "Amy Poehler, Maya Hawke, Phyllis Smith, Lewis Black", 96, "2024-06-14", "2024-08-31", "Mỹ", "P", "Ảnh/poster_inside_out_2.jpg", "https://www.youtube.com/watch?v=insideout2_trailer", "DangChieu"),
    (22, "Kẻ Trộm Mặt Trăng 4", "Gru, Lucy và các cô con gái chào đón thành viên mới Gru Jr., đồng thời phải trốn chạy kẻ thù truyền kiếp Maxime Le Mal vượt ngục.", "Chris Renaud", "Steve Carell, Kristen Wiig, Will Ferrell, Sofia Vergara", 94, "2024-07-05", "2024-09-15", "Mỹ", "P", "Ảnh/poster_despicable_me_4.jpg", "https://www.youtube.com/watch?v=despicableme4_trailer", "DangChieu"),
    (23, "Deadpool & Wolverine", "Wade Wilson tái xuất khi tổ chức TVA lôi kéo anh vào một nhiệm vụ bảo vệ dòng thời gian cùng biến thể Logan đầy miễn cưỡng.", "Shawn Levy", "Ryan Reynolds, Hugh Jackman, Emma Corrin, Matthew Macfadyen", 128, "2024-07-26", "2024-10-15", "Mỹ", "T18", "Ảnh/poster_deadpool_wolverine.jpg", "https://www.youtube.com/watch?v=deadpool_trailer", "DangChieu"),
    (24, "Quật Mộ Trùng Ma", "Hai pháp sư, một thầy phong thủy và một chuyên gia mai táng nhận lời khai quật một ngôi mộ bí ẩn tại vùng núi hẻo lánh và giải phóng ác linh.", "Jang Jae-hyun", "Choi Min-sik, Kim Go-eun, Yoo Hae-jin, Lee Do-hyun", 134, "2024-03-15", "2024-05-30", "Hàn Quốc", "T16", "Ảnh/poster_exhuma.jpg", "https://www.youtube.com/watch?v=exhuma_trailer", "DangChieu"),
    (25, "Gặp Lại Chị Bầu", "Phúc bất ngờ xuyên không về Sài Gòn thập niên 1990 và gặp gỡ nhóm bạn trẻ nhiệt huyết cùng Huyền - người mang thai đầy bí ẩn.", "Đoàn Nhất Trung", "Anh Tú, Diệu Nhi, Ngọc Phước, Quốc Khánh, Lê Giang", 114, "2024-02-10", "2024-04-10", "Việt Nam", "T13", "Ảnh/poster_gap_lai_chi_bau.jpg", "https://www.youtube.com/watch?v=gaplaichibau_trailer", "NgungChieu"),
    (26, "Mufasa: Vua Sư Tử", "Hành trình từ chú sư tử mồ côi đơn độc vươn lên thành một vị vua huyền thoại của Vùng Đất Kiêu Hãnh cùng Taka.", "Barry Jenkins", "Aaron Pierre, Kelvin Harrison Jr., Seth Rogen, Billy Eichner", 118, "2024-12-20", "2025-02-28", "Mỹ", "P", "Ảnh/poster_mufasa.jpg", "https://www.youtube.com/watch?v=mufasa_trailer", "SapChieu")
]

# 4. Phim_TheLoai (38 mappings >= 25)
movie_genres = [
    (1, 7), (1, 14), (1, 21),    # Mai: Drama, Romance, Tâm lý
    (2, 7), (2, 8),               # Lật Mặt 7: Drama, Gia đình
    (3, 10), (3, 14), (3, 18),    # Đào Phở Piano: Lịch sử, Lãng mạn, Chiến tranh
    (4, 4), (4, 7), (4, 8),       # Nhà Bà Nữ: Comedy, Drama, Gia đình
    (5, 7), (5, 8),               # Bố Già: Drama, Gia đình
    (6, 7), (6, 14),              # Mắt Biếc: Drama, Lãng mạn
    (7, 1), (7, 5), (7, 20),      # Hai Phượng: Hành động, Tội phạm, Võ thuật
    (8, 4), (8, 14),              # Em Chưa 18: Hài, Lãng mạn
    (9, 11), (9, 13),             # Quỷ Cẩu: Kinh dị, Bí ẩn
    (10, 11), (10, 23),           # Kẻ Ăn Hồn: Kinh dị, Thần thoại
    (11, 1), (11, 2), (11, 15),   # Dune 2: Hành động, Phiêu lưu, Sci-Fi
    (12, 6), (12, 7), (12, 10),   # Oppenheimer: Tài liệu, Drama, Lịch sử
    (13, 1), (13, 9), (13, 15),   # Godzilla x Kong: Hành động, Giả tưởng, Sci-Fi
    (14, 1), (14, 3), (14, 4),    # Kung Fu Panda 4: Hành động, Hoạt hình, Hài
    (15, 3), (15, 13), (15, 24),  # Conan: Hoạt hình, Bí ẩn, Trinh thám
    (16, 3), (16, 8), (16, 12),   # Doraemon: Hoạt hình, Gia đình, Âm nhạc
    (17, 1), (17, 2), (17, 15),   # Avatar 2: Hành động, Phiêu lưu, Sci-Fi
    (18, 1), (18, 9), (18, 22),   # Spider-Man: Hành động, Giả tưởng, Superhero
    (19, 1), (19, 15), (19, 22),  # Avengers Endgame: Hành động, Sci-Fi, Superhero
    (20, 7), (20, 15),            # Interstellar: Drama, Sci-Fi
    (21, 3), (21, 4), (21, 8),    # Inside Out 2: Hoạt hình, Hài, Gia đình
    (22, 3), (22, 4), (22, 8),    # Despicable Me 4: Hoạt hình, Hài, Gia đình
    (23, 1), (23, 4), (23, 22),   # Deadpool & Wolverine: Hành động, Hài, Superhero
    (24, 11), (24, 13), (24, 17), # Exhuma: Kinh dị, Bí ẩn, Giật gân
    (25, 4), (25, 7), (25, 14),   # Gặp Lại Chị Bầu: Hài, Drama, Lãng mạn
    (26, 2), (26, 3), (26, 8)     # Mufasa: Phiêu lưu, Hoạt hình, Gia đình
]

# 5. RapChieu (26 rows)
cinemas = [
    (1, "CGV Vincom Center Bà Triệu", "Tầng 6, Vincom Center, 191 Bà Triệu, Q. Hai Bà Trưng, Hà Nội", "02439743333", 1),
    (2, "CGV Vincom Center Đồng Khởi", "Tầng 3, 72 Lê Thánh Tôn, Bến Nghé, Quận 1, TP. Hồ Chí Minh", "02838275555", 1),
    (3, "CGV Landmark 81", "Tầng B1, Vincom Center Landmark 81, 720A Điện Biên Phủ, Bình Thạnh, TP. Hồ Chí Minh", "02836209999", 1),
    (4, "CGV Aeon Mall Long Biên", "Tầng 4, TTTM Aeon Mall Long Biên, 27 Cổ Linh, Long Biên, Hà Nội", "02437576999", 1),
    (5, "CGV Vincom Plaza Đà Nẵng", "Tầng 4, Vincom Plaza, 910A Ngô Quyền, Sơn Trà, TP. Đà Nẵng", "02363996888", 1),
    (6, "Lotte Cinema Keangnam", "Tầng 5, Keangnam Landmark Tower, Phạm Hùng, Nam Từ Liêm, Hà Nội", "02438378035", 1),
    (7, "Lotte Cinema Nam Sài Gòn", "Tầng 3, Lotte Mart, 469 Nguyễn Hữu Thọ, Tân Hưng, Quận 7, TP. Hồ Chí Minh", "02837752524", 1),
    (8, "Lotte Cinema Gò Vấp", "Tầng 3, Lotte Mart, 242 Nguyễn Văn Lượng, Phường 10, Gò Vấp, TP. Hồ Chí Minh", "02836202151", 1),
    (9, "Lotte Cinema Cần Thơ", "Tầng 1, Big C Cần Thơ, Lô số 1, KDC Hưng Phú 1, Cái Răng, TP. Cần Thơ", "02923917172", 1),
    (10, "Lotte Cinema Nha Trang Thái Nguyên", "Tầng 5, Maximark cũ, 60 Thái Nguyên, Phương Sài, TP. Nha Trang", "02583824881", 1),
    (11, "BHD Star Bitexco", "Tầng 3 & 4, Bitexco Icon 68, 2 Hải Triều, Bến Nghé, Quận 1, TP. Hồ Chí Minh", "02862670670", 1),
    (12, "BHD Star Vincom Phạm Ngọc Thạch", "Tầng 8, Vincom Center, 2 Phạm Ngọc Thạch, Đống Đa, Hà Nội", "02436373355", 1),
    (13, "BHD Star The Garden", "Tầng 4, TTTM The Garden, Mễ Trì, Nam Từ Liêm, Hà Nội", "02432068678", 1),
    (14, "BHD Star Discovery Cầu Giấy", "Tầng 8, Discovery Complex, 302 Cầu Giấy, Dịch Vọng, Hà Nội", "02432069678", 1),
    (15, "Galaxy Nguyễn Du", "116 Nguyễn Du, Phường Bến Thành, Quận 1, TP. Hồ Chí Minh", "02838234955", 1),
    (16, "Galaxy Tràng Thi", "10 Hai Bà Trưng, Phường Tràng Tiền, Hoàn Kiếm, Hà Nội", "02439387666", 1),
    (17, "Galaxy Tân Bình", "246 Nguyễn Hồng Đào, Phường 14, Tân Bình, TP. Hồ Chí Minh", "02838494567", 1),
    (18, "Galaxy Sala Thủ Thiêm", "Tầng 3, Thiso Mall, 10 Mai Chí Thọ, TP. Thủ Đức, TP. Hồ Chí Minh", "02836367890", 1),
    (19, "Trung Tâm Chiếu Phim Quốc Gia", "87 Láng Hạ, Chợ Dừa, Ba Đình, Hà Nội", "02435141791", 1),
    (20, "Cinestar Quốc Thanh", "271 Nguyễn Trãi, Phường Nguyễn Cư Trinh, Quận 1, TP. Hồ Chí Minh", "02873008881", 1),
    (21, "Cinestar Hai Bà Trưng", "135 Hai Bà Trưng, Bến Nghé, Quận 1, TP. Hồ Chí Minh", "02873008882", 1),
    (22, "Cinestar Sinh Viên Bình Dương", "Khuôn viên Nhà Văn Hóa Sinh Viên ĐHQG TP.HCM, Dĩ An, Bình Dương", "02747308883", 1),
    (23, "Beta Cinemas Đan Phượng", "Tầng 2, Tòa nhà HHA, KĐT Tân Tây Đô, Tân Lập, Đan Phượng, Hà Nội", "0934632682", 1),
    (24, "Beta Cinemas Thanh Xuân", "Tầng hầm B1, Tòa Golden West, 2 Lê Văn Thiêm, Thanh Xuân, Hà Nội", "0936081232", 1),
    (25, "Beta Cinemas Quang Trung", "645 Quang Trung, Phường 11, Gò Vấp, TP. Hồ Chí Minh", "0938472912", 1),
    (26, "Mega GS Cao Thắng", "Tầng 6-7, 19 Cao Thắng, Phường 2, Quận 3, TP. Hồ Chí Minh", "02862649911", 1)
]

# 6. PhongChieu (28 rows)
# (MaPhong, MaRap, TenPhong, LoaiPhong, SoLuongGhe)
rooms = [
    (1, 1, "Phòng 01 (IMAX)", "IMAX", 150),
    (2, 1, "Phòng 02 (2D Standard)", "2D", 120),
    (3, 2, "Phòng 01 (ScreenX)", "ScreenX", 130),
    (4, 2, "Phòng 02 (Gold Class)", "VIP", 60),
    (5, 3, "Phòng IMAX Laser", "IMAX", 200),
    (6, 3, "Phòng 02 (3D Atmos)", "3D", 140),
    (7, 4, "Phòng Chiếu 1", "2D", 100),
    (8, 4, "Phòng Chiếu 2", "3D", 100),
    (9, 5, "Phòng 01 Đà Nẵng", "2D", 120),
    (10, 6, "Phòng Superplex", "Superplex", 180),
    (11, 7, "Phòng 01 Nam Sài Gòn", "2D", 110),
    (12, 8, "Phòng 01 Gò Vấp", "3D", 110),
    (13, 9, "Phòng 01 Cần Thơ", "2D", 90),
    (14, 10, "Phòng 01 Nha Trang", "2D", 90),
    (15, 11, "Phòng First Class", "VIP", 50),
    (16, 12, "Phòng 01 BHD Phạm Ngọc Thạch", "2D", 120),
    (17, 13, "Phòng 01 BHD The Garden", "2D", 100),
    (18, 14, "Phòng 01 BHD Cầu Giấy", "2D", 100),
    (19, 15, "Phòng 01 Galaxy Nguyễn Du", "2D", 140),
    (20, 16, "Phòng 01 Galaxy Tràng Thi", "2D", 110),
    (21, 17, "Phòng 01 Galaxy Tân Bình", "2D", 120),
    (22, 18, "Phòng IMAX Laser Sala", "IMAX", 180),
    (23, 19, "Phòng Chiếu Số 1 Quốc Gia", "2D", 250),
    (24, 20, "Phòng 01 Cinestar Quốc Thanh", "2D", 120),
    (25, 21, "Phòng 01 Cinestar Hai Bà Trưng", "2D", 100),
    (26, 22, "Phòng 01 Sinh Viên", "2D", 150),
    (27, 23, "Phòng 01 Beta Đan Phượng", "2D", 100),
    (28, 24, "Phòng 01 Beta Thanh Xuân", "2D", 120)
]

# 7. GheNgoi (40 rows)
# (MaGhe, MaPhong, HangGhe, SoGhe, LoaiGhe, TrangThai)
# In Room 1: 36 seats
seats = []
seat_id = 1
# Phòng 1: Hàng A, B (Thường), C, D (VIP), E (Đôi)
for hang in ['A', 'B']:
    for num in range(1, 9):
        seats.append((seat_id, 1, hang, num, "Thuong", 1))
        seat_id += 1
for hang in ['C', 'D']:
    for num in range(1, 9):
        seats.append((seat_id, 1, hang, num, "VIP", 1))
        seat_id += 1
for num in range(1, 5):
    seats.append((seat_id, 1, 'E', num, "Doi", 1))
    seat_id += 1

# Phòng 2: 4 ghế
for num in range(1, 5):
    seats.append((seat_id, 2, 'A', num, "Thuong", 1))
    seat_id += 1

# 8. SuatChieu (28 rows)
# (MaSuatChieu, MaPhim, MaPhong, NgayChieu, GioBatDau, GioKetThuc, GiaVe, TrangThai)
# All GioKetThuc > GioBatDau
showtimes = [
    (1, 11, 1, "2026-11-04", "08:30:00", "11:15:00", 120000.00, "DangMoBan"),
    (2, 11, 1, "2026-11-04", "11:45:00", "14:30:00", 120000.00, "DangMoBan"),
    (3, 13, 1, "2026-11-04", "15:00:00", "17:15:00", 120000.00, "DangMoBan"),
    (4, 13, 1, "2026-11-04", "18:00:00", "20:15:00", 140000.00, "DangMoBan"),
    (5, 23, 1, "2026-11-04", "20:45:00", "23:00:00", 150000.00, "DangMoBan"),

    (6, 14, 2, "2026-11-04", "09:00:00", "10:45:00", 85000.00, "DangMoBan"),
    (7, 15, 2, "2026-11-04", "11:15:00", "13:15:00", 85000.00, "DangMoBan"),
    (8, 16, 2, "2026-11-04", "14:00:00", "16:00:00", 85000.00, "DangMoBan"),
    (9, 21, 2, "2026-11-04", "16:30:00", "18:15:00", 95000.00, "DangMoBan"),
    (10, 24, 2, "2026-11-04", "19:00:00", "21:30:00", 100000.00, "DangMoBan"),

    (11, 22, 3, "2026-11-05", "09:30:00", "11:15:00", 90000.00, "DangMoBan"),
    (12, 11, 3, "2026-11-05", "13:00:00", "15:45:00", 110000.00, "DangMoBan"),
    (13, 23, 3, "2026-11-05", "16:30:00", "18:45:00", 120000.00, "DangMoBan"),
    (14, 24, 4, "2026-11-05", "19:30:00", "22:00:00", 180000.00, "DangMoBan"),

    (15, 13, 5, "2026-11-05", "10:00:00", "12:15:00", 150000.00, "DangMoBan"),
    (16, 11, 5, "2026-11-05", "14:00:00", "16:45:00", 160000.00, "DangMoBan"),
    (17, 23, 5, "2026-11-05", "18:30:00", "20:45:00", 180000.00, "DangMoBan"),

    (18, 14, 6, "2026-11-06", "09:00:00", "10:45:00", 95000.00, "DangMoBan"),
    (19, 15, 7, "2026-11-06", "13:30:00", "15:30:00", 80000.00, "DangMoBan"),
    (20, 16, 8, "2026-11-06", "16:00:00", "18:00:00", 85000.00, "DangMoBan"),
    (21, 21, 9, "2026-11-06", "19:00:00", "20:45:00", 90000.00, "DangMoBan"),
    (22, 24, 10, "2026-11-06", "20:30:00", "23:00:00", 110000.00, "DangMoBan"),

    (23, 11, 11, "2026-11-07", "09:00:00", "11:45:00", 85000.00, "DangMoBan"),
    (24, 13, 12, "2026-11-07", "13:00:00", "15:15:00", 90000.00, "DangMoBan"),
    (25, 23, 15, "2026-11-07", "18:00:00", "20:15:00", 200000.00, "DangMoBan"),
    (26, 24, 19, "2026-11-07", "20:30:00", "23:00:00", 90000.00, "DangMoBan"),
    (27, 26, 22, "2026-12-25", "10:00:00", "12:00:00", 160000.00, "SapChieu"),
    (28, 26, 23, "2026-12-25", "14:00:00", "16:00:00", 100000.00, "SapChieu")
]

# 9. GheSuatChieu (32 rows >= 25)
# (MaGheSuatChieu, MaSuatChieu, MaGhe, GiaVe, TrangThai, ThoiGianHetHan)
# 1..26 are DaDat, 27..30 are DangGiu, 31..32 are Trong
showtime_seats = []
for i in range(1, 27):
    # Map to SuatChieu 1, 2, 3... and Ghe 1..26
    # MaSuatChieu = (i % 5) + 1 (SuatChieu 1 to 5)
    # MaGhe = i
    suat = ((i - 1) % 5) + 1
    # Giá vé theo loại ghế
    if i <= 16:
        price = 120000.00
    elif i <= 32:
        price = 140000.00
    else:
        price = 220000.00
    showtime_seats.append((i, suat, i, price, "DaDat", "NULL"))

# DangGiu
showtime_seats.append((27, 1, 27, 140000.00, "DangGiu", "'2026-11-04 08:45:00'"))
showtime_seats.append((28, 1, 28, 140000.00, "DangGiu", "'2026-11-04 08:45:00'"))
showtime_seats.append((29, 2, 29, 140000.00, "DangGiu", "'2026-11-04 11:55:00'"))
showtime_seats.append((30, 2, 30, 140000.00, "DangGiu", "'2026-11-04 11:55:00'"))

# Trong
showtime_seats.append((31, 1, 31, 140000.00, "Trong", "NULL"))
showtime_seats.append((32, 1, 32, 140000.00, "Trong", "NULL"))


# 10. DatVe (26 rows)
# (MaDatVe, MaNguoiDung, MaSuatChieu, NgayDat, TongTien, TrangThai)
# Status: ChoThanhToan, DaThanhToan, DaHuy, HetHan
bookings = []
for i in range(1, 27):
    user_id = (i % 22) + 7  # Customer users 7 to 28
    suat_id = ((i - 1) % 5) + 1
    price = showtime_seats[i - 1][3]
    if i <= 22:
        status = "DaThanhToan"
    elif i <= 24:
        status = "ChoThanhToan"
    elif i == 25:
        status = "DaHuy"
    else:
        status = "HetHan"
    day = 1 + (i % 3)
    order_date = f"2026-11-0{day} {10 + (i % 10):02d}:{15 + (i * 2) % 40:02d}:00"
    bookings.append((i, user_id, suat_id, order_date, price, status))


# 11. ChiTietDatVe (26 rows)
# (MaChiTiet, MaDatVe, MaGheSuatChieu, DonGia)
# UNIQUE (MaGheSuatChieu)
booking_details = []
for i in range(1, 27):
    price = showtime_seats[i - 1][3]
    booking_details.append((i, i, i, price))


# 12. ThanhToan (26 rows)
# (MaThanhToan, MaDatVe, PhuongThuc, MaGiaoDich, SoTien, NgayThanhToan, TrangThai)
# Status: ChoXuLy, ThanhCong, ThatBai, HoanTien
payments = []
pmethods = ["MoMo", "VNPay", "ZaloPay", "TheTinDung", "ShopeePay"]
for i in range(1, 27):
    datve = bookings[i - 1]
    amount = datve[4]
    bstatus = datve[5]
    pmethod = pmethods[i % len(pmethods)]
    txn_code = f"TXN2026110{1 + (i % 3)}{i:05d}"
    pay_date = datve[3]
    if bstatus == "DaThanhToan":
        pstatus = "ThanhCong"
    elif bstatus == "ChoThanhToan":
        pstatus = "ChoXuLy"
    elif bstatus == "DaHuy":
        pstatus = "HoanTien"
    else:
        pstatus = "ThatBai"
    payments.append((i, i, pmethod, txn_code, amount, pay_date, pstatus))


# 13. VeDienTu (26 rows)
# (MaVe, MaChiTiet, MaQR, NgayPhatHanh, TrangThai, NgaySuDung)
# UNIQUE (MaChiTiet), UNIQUE (MaQR)
# Status: ChuaSuDung, DaSuDung, DaHuy
etickets = []
for i in range(1, 27):
    bstatus = bookings[i - 1][5]
    qr_code = f"QR-CSDL-2026-TICKET-{i:04d}-{1000 + i}"
    issue_date = bookings[i - 1][3]
    if bstatus == "DaThanhToan":
        if i <= 10:
            ticket_status = "DaSuDung"
            use_date = f"'2026-11-04 18:00:00'"
        else:
            ticket_status = "ChuaSuDung"
            use_date = "NULL"
    elif bstatus == "DaHuy":
        ticket_status = "DaHuy"
        use_date = "NULL"
    else:
        ticket_status = "ChuaSuDung"
        use_date = "NULL"
    etickets.append((i, i, qr_code, issue_date, ticket_status, use_date))


# 14. ThongBao (26 rows)
# (MaThongBao, MaNguoiDung, TieuDe, NoiDung, NgayGui, DaDoc)
notifications = [
    (1, 1, "Chào mừng Quản trị viên", "Hệ thống quản lý cụm rạp chiếu phim đã sẵn sàng đi vào vận hành.", "2024-01-01 08:30:00", 1),
    (2, 2, "Cập nhật chính sách giá vé", "Chính sách giá vé áp dụng cho các suất chiếu cuối tuần đã được cập nhật.", "2024-01-02 10:00:00", 1),
    (3, 3, "Phân ca trực phòng vé", "Bạn đã được phân công ca trực sáng thứ 7 tại rạp CGV Vincom Bà Triệu.", "2024-01-06 08:00:00", 1),
    (4, 4, "Hướng dẫn quét mã vé QR", "Vui lòng xem tài liệu hướng dẫn mới về việc xác thực mã QR vé điện tử trên app nhân viên.", "2024-01-06 09:00:00", 1),
    (5, 7, "Đặt vé thành công", "Bạn đã thanh toán thành công vé xem phim Mai. Mã vé QR đã được tạo.", "2026-11-01 10:20:00", 1),
    (6, 8, "Đặt vé thành công", "Bạn đã thanh toán thành công vé xem phim Lật Mặt 7. Vui lòng xuất trình mã QR tại quầy soát vé.", "2026-11-01 11:30:00", 1),
    (7, 9, "Nhắc nhở suất chiếu sắp tới", "Suất chiếu phim Dune 2 của bạn sẽ bắt đầu trong 60 phút nữa.", "2026-11-02 07:30:00", 1),
    (8, 10, "Ưu đãi thành viên CGV Member", "Tặng bạn voucher giảm 20% combo bỏng nước cho lần đặt vé kế tiếp.", "2026-11-02 09:00:00", 0),
    (9, 11, "Xác nhận hoàn tiền", "Yêu cầu hoàn tiền vé xem phim của bạn đã được xử lý thành công qua MoMo.", "2026-11-02 14:00:00", 1),
    (10, 12, "Vé điện tử của bạn đã sẵn sàng", "Vé xem phim Godzilla x Kong tại phòng chiếu IMAX đã sẵn sàng trong mục Vé của tôi.", "2026-11-02 16:00:00", 0),
    (11, 13, "Cảnh báo hết hạn giữ ghế", "Suất chiếu của bạn chỉ còn 5 phút để hoàn tất thanh toán trước khi ghế bị hủy.", "2026-11-03 08:40:00", 1),
    (12, 14, "Đánh giá phim sau khi xem", "Bạn thấy bộ phim Kung Fu Panda 4 thế nào? Hãy chia sẻ đánh giá để nhận 50 điểm tích lũy.", "2026-11-03 10:15:00", 0),
    (13, 15, "Thanh toán thành công", "Giao dịch VNPay số tiền 120,000đ cho mã đặt vé #15 đã thành công.", "2026-11-03 11:00:00", 1),
    (14, 16, "Phim mới sắp chiếu", "Bom tấn Mufasa: Vua Sư Tử chính thức mở bán vé sớm. Đặt ngay để có chỗ ngồi ưng ý!", "2026-11-03 14:00:00", 0),
    (15, 17, "Đặt vé thành công", "Bạn đã đặt thành công ghế C-01 tại CGV Vincom Bà Triệu.", "2026-11-03 15:30:00", 0),
    (16, 18, "Cập nhật ứng dụng phiên bản mới", "Tính năng chọn combo bắp nước trực tiếp khi đặt vé đã có mặt trên phiên bản mới.", "2026-11-03 16:00:00", 0),
    (17, 19, "Chúc mừng sinh nhật thành viên", "Tặng bạn 01 vé xem phim 2D miễn phí nhân dịp sinh nhật trong tháng này!", "2026-11-03 16:30:00", 1),
    (18, 20, "Xác nhận đặt vé #20", "Vé điện tử xem phim Đào, Phở và Piano đã được gửi về email của bạn.", "2026-11-03 17:00:00", 0),
    (19, 21, "Thông báo bảo trì hệ thống", "Hệ thống thanh toán qua ZaloPay sẽ tạm dừng nâng cấp từ 01:00 đến 03:00 ngày mai.", "2026-11-03 17:15:00", 0),
    (20, 22, "Ưu đãi giá vé thứ 4 vui vẻ", "Đồng giá vé chỉ từ 50k cho tất cả các suất chiếu 2D vào thứ 4 hàng tuần.", "2026-11-03 17:20:00", 0),
    (21, 23, "Thanh toán thất bại", "Giao dịch thanh toán vé #24 không thành công do thẻ hết hạn mức. Vui lòng thử lại.", "2026-11-03 17:25:00", 1),
    (22, 24, "Vé đã sử dụng", "Mã QR vé xem phim #1 đã được nhân viên kiểm soát vé quét xác thực thành công.", "2026-11-03 17:30:00", 1),
    (23, 25, "Vé của bạn bị hủy do quá hạn", "Đơn đặt vé #26 đã tự động bị hủy do không thanh toán trong thời gian quy định.", "2026-11-03 17:35:00", 1),
    (24, 26, "Đánh giá nhận voucher", "Cảm ơn bạn đã đóng góp đánh giá cho phim Quật Mộ Trùng Ma (Exhuma).", "2026-11-03 17:40:00", 0),
    (25, 27, "Chào mừng thành viên mới", "Tài khoản của bạn đã được đăng ký thành công trên Hệ Thống Đặt Vé Xem Phim.", "2026-11-03 17:45:00", 1),
    (26, 28, "Đăng nhập từ thiết bị mới", "Phát hiện đăng nhập tài khoản của bạn trên trình duyệt Chrome lúc 17:00.", "2026-11-03 17:50:00", 0)
]

# 15. DanhGiaPhim (26 rows)
# (MaDanhGia, MaNguoiDung, MaPhim, SoSao, BinhLuan, NgayDanhGia)
# UNIQUE (MaNguoiDung, MaPhim), CHECK SoSao BETWEEN 1 AND 5
reviews = [
    (1, 7, 1, 5, "Phim rất xúc động, diễn xuất của Phương Anh Đào và Tuấn Trần quá đỉnh cao. Kịch bản chạm tới trái tim người xem.", "2024-02-15 14:00:00"),
    (2, 8, 2, 5, "Lý Hải làm phim gia đình cảm động rơi nước mắt. Cảnh mẹ già đợi các con quá thương tâm.", "2024-05-01 16:30:00"),
    (3, 9, 3, 4, "Hình ảnh Hà Nội xưa hào hùng và lãng mạn. Âm nhạc và góc quay rất nghệ thuật.", "2024-02-20 20:15:00"),
    (4, 10, 4, 4, "Phim phản ánh đúng thực tế gia đình Việt, lời thoại chân thật nhưng hơi nhiều tiếng chửi một chút.", "2023-01-25 18:00:00"),
    (5, 11, 5, 5, "Xem khóc sưng cả mắt, tình cảm cha con được thể hiện vô cùng mộc mạc và chân thành.", "2021-03-10 19:45:00"),
    (6, 12, 6, 5, "Nhạc phim Phan Mạnh Quỳnh quá tuyệt vời. Diễn viên Ngạn và Hà Lan như bước ra từ trang sách.", "2020-01-05 21:00:00"),
    (7, 13, 7, 4, "Hành động mãn nhãn, đẳng cấp quốc tế của Ngô Thanh Vân. Đánh đấm rất có lực và nghẹt thở.", "2019-03-01 22:15:00"),
    (8, 14, 8, 4, "Phim hài hước trẻ trung, giải trí rất tốt. Kiều Minh Tuấn và Kaity Nguyễn diễn xuất ăn ý bất ngờ.", "2017-05-05 15:30:00"),
    (9, 15, 9, 3, "Ý tưởng văn hóa dân gian hay, không khí rùng rợn nhưng kỹ xảo CGI còn hơi giả.", "2024-01-10 17:20:00"),
    (10, 16, 10, 4, "Bối cảnh Làng Địa Ngục dựng rất kỳ công và rùng rợn. Phục trang truyền thống đẹp mắt.", "2023-12-20 18:40:00"),
    (11, 17, 11, 5, "Một kiệt tác điện ảnh khoa học viễn tưởng thế kỷ 21! Hình ảnh và âm thanh của Hans Zimmer quá choáng ngợp trên màn IMAX.", "2024-03-05 20:00:00"),
    (12, 18, 12, 5, "Christopher Nolan không bao giờ làm khán giả thất vọng. Cillian Murphy xứng đáng với giải Oscar.", "2023-08-15 16:00:00"),
    (13, 19, 13, 4, "Hành động đã mắt, giải trí đỉnh cao. Đánh nhau từ đầu đến cuối xem cực kỳ phấn khích.", "2024-04-02 14:15:00"),
    (14, 20, 14, 4, "Po vẫn giữ được nét đáng yêu quen thuộc. Đồ họa rực rỡ, các pha võ thuật kungfu đẹp mắt.", "2024-03-12 11:30:00"),
    (15, 21, 15, 5, "Phần Conan hay nhất trong vài năm trở lại đây. Cặp đôi Kaito Kid và Heiji tương tác cực kỳ thú vị.", "2024-08-05 19:10:00"),
    (16, 22, 16, 5, "Âm nhạc lay động tâm hồn, thông điệp bảo vệ thế giới và sức mạnh của âm nhạc rất nhân văn.", "2024-05-28 15:00:00"),
    (17, 23, 17, 5, "Hình ảnh đại dương Pandora đẹp đến nghẹt thở. Trải nghiệm 3D tuyệt đỉnh không đâu sánh bằng.", "2022-12-20 18:30:00"),
    (18, 24, 18, 5, "Khoảnh khắc cả 3 người nhện cùng xuất hiện làm cả rạp chiếu vỗ tay bùng nổ. Cảm xúc trọn vẹn!", "2021-12-22 21:40:00"),
    (19, 25, 19, 5, "Hồi kết vĩ đại cho hành trình 10 năm MCU. I love you 3000 Tony Stark!", "2019-05-02 20:30:00"),
    (20, 26, 20, 5, "Bộ phim hay nhất mọi thời đại về vũ trụ và tình cha con. Cảnh quay hố đen Gargantua quá vĩ đại.", "2014-11-15 19:00:00"),
    (21, 27, 21, 5, "Các nhân vật cảm xúc mới như Lo Âu (Anxiety) được xây dựng quá xuất sắc, đánh trúng tâm lý tuổi trẻ.", "2024-06-20 17:45:00"),
    (22, 28, 22, 4, "Hài hước dí dỏm, lũ Minions siêu quậy cười đau cả bụng. Rất thích hợp đi xem cùng gia đình.", "2024-07-10 14:00:00"),
    (23, 7, 23, 5, "Màn kết hợp bùng nổ giữa Deadpool và Wolverine, ngập tràn các vai cameo bất ngờ và những pha chém giết hành động đỉnh chóp.", "2024-07-30 22:00:00"),
    (24, 8, 24, 5, "Phim kinh dị phong thủy của Hàn Quốc cực kỳ lôi cuốn, dàn diễn viên gạo cội nhập vai xuất thần.", "2024-03-20 18:15:00"),
    (25, 9, 25, 4, "Phim ấm áp, tái hiện lại bối cảnh Sài Gòn thập niên 90 rất thơ và hoài niệm.", "2024-02-18 16:20:00"),
    (26, 10, 26, 5, "Rất hóng chờ phim Mufasa ra mắt, teaser trailer quá ấn tượng!", "2024-11-01 10:00:00")
]

# Tạo file SQL
sql_file_path = os.path.join(os.getcwd(), "CSDL_DatVeXemPhim_InsertData.sql")
with open(sql_file_path, "w", encoding="utf-8-sig") as f:
    f.write("""-- =========================================================================
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
""")
    for i, u in enumerate(users):
        comma = "," if i < len(users) - 1 else ";"
        f.write(f"({u[0]}, N'{u[1]}', '{u[2]}', '{u[3]}', '{u[4]}', N'{u[5]}', '{u[6]}', {u[7]}){comma}\n")

    f.write("""SET IDENTITY_INSERT NguoiDung OFF;
GO

-- =========================================================================
-- 2. BẢNG TheLoai (26 bản ghi >= 25)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng TheLoai...';
SET IDENTITY_INSERT TheLoai ON;

INSERT INTO TheLoai (MaTheLoai, TenTheLoai) VALUES
""")
    for i, g in enumerate(genres):
        comma = "," if i < len(genres) - 1 else ";"
        f.write(f"({g[0]}, N'{g[1]}'){comma}\n")

    f.write("""SET IDENTITY_INSERT TheLoai OFF;
GO

-- =========================================================================
-- 3. BẢNG Phim (26 bản ghi >= 25)
-- Ảnh poster lưu trong folder 'Ảnh/' (vd: Ảnh/poster_mai.jpg)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng Phim...';
SET IDENTITY_INSERT Phim ON;

INSERT INTO Phim (MaPhim, TenPhim, MoTa, DaoDien, DienVien, ThoiLuong, NgayKhoiChieu, NgayKetThuc, QuocGia, DoTuoi, Poster, Trailer, TrangThai) VALUES
""")
    for i, m in enumerate(movies):
        comma = "," if i < len(movies) - 1 else ";"
        f.write(f"({m[0]}, N'{m[1]}', N'{m[2]}', N'{m[3]}', N'{m[4]}', {m[5]}, '{m[6]}', '{m[7]}', N'{m[8]}', N'{m[9]}', N'{m[10]}', N'{m[11]}', N'{m[12]}'){comma}\n")

    f.write("""SET IDENTITY_INSERT Phim OFF;
GO

-- =========================================================================
-- 4. BẢNG Phim_TheLoai (71 bản ghi >= 25)
-- Bảng liên kết Phim và Thể loại
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng Phim_TheLoai...';

INSERT INTO Phim_TheLoai (MaPhim, MaTheLoai) VALUES
""")
    for i, mg in enumerate(movie_genres):
        comma = "," if i < len(movie_genres) - 1 else ";"
        f.write(f"({mg[0]}, {mg[1]}){comma}\n")

    f.write("""GO

-- =========================================================================
-- 5. BẢNG RapChieu (26 bản ghi >= 25)
-- Cụm rạp CGV, Lotte, BHD, Galaxy, Beta, Cinestar,...
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng RapChieu...';
SET IDENTITY_INSERT RapChieu ON;

INSERT INTO RapChieu (MaRap, TenRap, DiaChi, SoDienThoai, TrangThai) VALUES
""")
    for i, c in enumerate(cinemas):
        comma = "," if i < len(cinemas) - 1 else ";"
        f.write(f"({c[0]}, N'{c[1]}', N'{c[2]}', '{c[3]}', {c[4]}){comma}\n")

    f.write("""SET IDENTITY_INSERT RapChieu OFF;
GO

-- =========================================================================
-- 6. BẢNG PhongChieu (28 bản ghi >= 25)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng PhongChieu...';
SET IDENTITY_INSERT PhongChieu ON;

INSERT INTO PhongChieu (MaPhong, MaRap, TenPhong, LoaiPhong, SoLuongGhe) VALUES
""")
    for i, r in enumerate(rooms):
        comma = "," if i < len(rooms) - 1 else ";"
        f.write(f"({r[0]}, {r[1]}, N'{r[2]}', N'{r[3]}', {r[4]}){comma}\n")

    f.write("""SET IDENTITY_INSERT PhongChieu OFF;
GO

-- =========================================================================
-- 7. BẢNG GheNgoi (40 bản ghi >= 25)
-- Các loại ghế: Thuong, VIP, Doi (Sweetbox)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng GheNgoi...';
SET IDENTITY_INSERT GheNgoi ON;

INSERT INTO GheNgoi (MaGhe, MaPhong, HangGhe, SoGhe, LoaiGhe, TrangThai) VALUES
""")
    for i, s in enumerate(seats):
        comma = "," if i < len(seats) - 1 else ";"
        f.write(f"({s[0]}, {s[1]}, '{s[2]}', {s[3]}, N'{s[4]}', {s[5]}){comma}\n")

    f.write("""SET IDENTITY_INSERT GheNgoi OFF;
GO

-- =========================================================================
-- 8. BẢNG SuatChieu (28 bản ghi >= 25)
-- Đảm bảo check constraint GioKetThuc > GioBatDau và GiaVe >= 0
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng SuatChieu...';
SET IDENTITY_INSERT SuatChieu ON;

INSERT INTO SuatChieu (MaSuatChieu, MaPhim, MaPhong, NgayChieu, GioBatDau, GioKetThuc, GiaVe, TrangThai) VALUES
""")
    for i, sc in enumerate(showtimes):
        comma = "," if i < len(showtimes) - 1 else ";"
        f.write(f"({sc[0]}, {sc[1]}, {sc[2]}, '{sc[3]}', '{sc[4]}', '{sc[5]}', {sc[6]:.2f}, N'{sc[7]}'){comma}\n")

    f.write("""SET IDENTITY_INSERT SuatChieu OFF;
GO

-- =========================================================================
-- 9. BẢNG GheSuatChieu (32 bản ghi >= 25)
-- TrangThai: Trong, DangGiu, DaDat
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng GheSuatChieu...';
SET IDENTITY_INSERT GheSuatChieu ON;

INSERT INTO GheSuatChieu (MaGheSuatChieu, MaSuatChieu, MaGhe, GiaVe, TrangThai, ThoiGianHetHan) VALUES
""")
    for i, gs in enumerate(showtime_seats):
        comma = "," if i < len(showtime_seats) - 1 else ";"
        exp_time = gs[5]
        f.write(f"({gs[0]}, {gs[1]}, {gs[2]}, {gs[3]:.2f}, N'{gs[4]}', {exp_time}){comma}\n")

    f.write("""SET IDENTITY_INSERT GheSuatChieu OFF;
GO

-- =========================================================================
-- 10. BẢNG DatVe (26 bản ghi >= 25)
-- TrangThai: ChoThanhToan, DaThanhToan, DaHuy, HetHan
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng DatVe...';
SET IDENTITY_INSERT DatVe ON;

INSERT INTO DatVe (MaDatVe, MaNguoiDung, MaSuatChieu, NgayDat, TongTien, TrangThai) VALUES
""")
    for i, b in enumerate(bookings):
        comma = "," if i < len(bookings) - 1 else ";"
        f.write(f"({b[0]}, {b[1]}, {b[2]}, '{b[3]}', {b[4]:.2f}, N'{b[5]}'){comma}\n")

    f.write("""SET IDENTITY_INSERT DatVe OFF;
GO

-- =========================================================================
-- 11. BẢNG ChiTietDatVe (26 bản ghi >= 25)
-- UNIQUE (MaGheSuatChieu)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng ChiTietDatVe...';
SET IDENTITY_INSERT ChiTietDatVe ON;

INSERT INTO ChiTietDatVe (MaChiTiet, MaDatVe, MaGheSuatChieu, DonGia) VALUES
""")
    for i, bd in enumerate(booking_details):
        comma = "," if i < len(booking_details) - 1 else ";"
        f.write(f"({bd[0]}, {bd[1]}, {bd[2]}, {bd[3]:.2f}){comma}\n")

    f.write("""SET IDENTITY_INSERT ChiTietDatVe OFF;
GO

-- =========================================================================
-- 12. BẢNG ThanhToan (26 bản ghi >= 25)
-- TrangThai: ChoXuLy, ThanhCong, ThatBai, HoanTien
-- PhuongThuc: MoMo, VNPay, ZaloPay, TheTinDung, ShopeePay
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng ThanhToan...';
SET IDENTITY_INSERT ThanhToan ON;

INSERT INTO ThanhToan (MaThanhToan, MaDatVe, PhuongThuc, MaGiaoDich, SoTien, NgayThanhToan, TrangThai) VALUES
""")
    for i, p in enumerate(payments):
        comma = "," if i < len(payments) - 1 else ";"
        f.write(f"({p[0]}, {p[1]}, N'{p[2]}', '{p[3]}', {p[4]:.2f}, '{p[5]}', N'{p[6]}'){comma}\n")

    f.write("""SET IDENTITY_INSERT ThanhToan OFF;
GO

-- =========================================================================
-- 13. BẢNG VeDienTu (26 bản ghi >= 25)
-- TrangThai: ChuaSuDung, DaSuDung, DaHuy
-- UNIQUE (MaChiTiet), UNIQUE (MaQR)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng VeDienTu...';
SET IDENTITY_INSERT VeDienTu ON;

INSERT INTO VeDienTu (MaVe, MaChiTiet, MaQR, NgayPhatHanh, TrangThai, NgaySuDung) VALUES
""")
    for i, vt in enumerate(etickets):
        comma = "," if i < len(etickets) - 1 else ";"
        f.write(f"({vt[0]}, {vt[1]}, '{vt[2]}', '{vt[3]}', N'{vt[4]}', {vt[5]}){comma}\n")

    f.write("""SET IDENTITY_INSERT VeDienTu OFF;
GO

-- =========================================================================
-- 14. BẢNG ThongBao (26 bản ghi >= 25)
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng ThongBao...';
SET IDENTITY_INSERT ThongBao ON;

INSERT INTO ThongBao (MaThongBao, MaNguoiDung, TieuDe, NoiDung, NgayGui, DaDoc) VALUES
""")
    for i, n in enumerate(notifications):
        comma = "," if i < len(notifications) - 1 else ";"
        f.write(f"({n[0]}, {n[1]}, N'{n[2]}', N'{n[3]}', '{n[4]}', {n[5]}){comma}\n")

    f.write("""SET IDENTITY_INSERT ThongBao OFF;
GO

-- =========================================================================
-- 15. BẢNG DanhGiaPhim (26 bản ghi >= 25)
-- UNIQUE (MaNguoiDung, MaPhim), CHECK SoSao BETWEEN 1 AND 5
-- =========================================================================
PRINT N'Đang chèn dữ liệu bảng DanhGiaPhim...';
SET IDENTITY_INSERT DanhGiaPhim ON;

INSERT INTO DanhGiaPhim (MaDanhGia, MaNguoiDung, MaPhim, SoSao, BinhLuan, NgayDanhGia) VALUES
""")
    for i, r in enumerate(reviews):
        comma = "," if i < len(reviews) - 1 else ";"
        f.write(f"({r[0]}, {r[1]}, {r[2]}, {r[3]}, N'{r[4]}', '{r[5]}'){comma}\n")

    f.write("""SET IDENTITY_INSERT DanhGiaPhim OFF;
GO

PRINT N'=========================================================================';
PRINT N'===> ĐÃ CHÈN TOÀN BỘ DỮ LIỆU MẪU CHO 15 BẢNG THÀNH CÔNG! (Mỗi bảng >= 25 dòng)';
PRINT N'=========================================================================';
GO
""")

print("Đã tạo file SQL thành công tại:", sql_file_path)


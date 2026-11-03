# -*- coding: utf-8 -*-
import os
import sys

sys.stdout.reconfigure(encoding='utf-8')
from PIL import Image, ImageDraw, ImageFont

# 1. Tạo thư mục Ảnh
image_dir = os.path.join(os.getcwd(), "Ảnh")
os.makedirs(image_dir, exist_ok=True)
print(f"Thư mục ảnh: {image_dir}")

# Danh sách 26 phim
movies_info = [
    {
        "id": 1, "title": "Mai", "english_title": "MAI",
        "desc": "Câu chuyện xoay quanh Mai, một phụ nữ gần 40 tuổi làm nghề massage, phải đối mặt với định kiến xã hội và tìm kiếm hạnh phúc đích thực.",
        "director": "Trấn Thành", "actors": "Phương Anh Đào, Tuấn Trần, Trấn Thành, Hồng Đào",
        "duration": 131, "release": "2024-02-10", "end": "2024-04-10", "country": "Việt Nam",
        "rating": "T18", "status": "NgungChieu", "poster_file": "poster_mai.jpg",
        "trailer": "https://www.youtube.com/watch?v=mai_trailer", "bg_color": (140, 30, 45)
    },
    {
        "id": 2, "title": "Lật Mặt 7: Một Điều Ước", "english_title": "ONE WISH",
        "desc": "Khắc họa tình cảm gia đình cảm động của người mẹ già cùng 5 người con trưởng thành với những hoàn cảnh và số phận khác nhau.",
        "director": "Lý Hải", "actors": "Thanh Hiền, Trương Minh Cường, Đinh Y Nhung, Quách Ngọc Tuyên",
        "duration": 138, "release": "2024-04-26", "end": "2024-06-30", "country": "Việt Nam",
        "rating": "K", "status": "NgungChieu", "poster_file": "poster_lat_mat_7.jpg",
        "trailer": "https://www.youtube.com/watch?v=latmat7_trailer", "bg_color": (34, 112, 94)
    },
    {
        "id": 3, "title": "Đào, Phở và Piano", "english_title": "PEACH BLOSSOM, PHO & PIANO",
        "desc": "Bức tranh bi tráng và lãng mạn về tình yêu thời chiến trong bối cảnh cuộc chiến đấu bảo vệ Hà Nội mùa đông năm 1946.",
        "director": "Phi Tiến Sơn", "actors": "Doãn Quốc Đam, Cao Thị Thùy Linh, Trần Lực, Tuấn Hưng",
        "duration": 100, "release": "2024-02-10", "end": "2024-04-15", "country": "Việt Nam",
        "rating": "T13", "status": "NgungChieu", "poster_file": "poster_dao_pho_piano.jpg",
        "trailer": "https://www.youtube.com/watch?v=daophopiano_trailer", "bg_color": (120, 60, 30)
    },
    {
        "id": 4, "title": "Nhà Bà Nữ", "english_title": "THE HOUSE OF NO MAN",
        "desc": "Mâu thuẫn thế hệ và áp lực gia đình trong một tiệm bánh canh cua nổi tiếng tại Sài Gòn do một người mẹ độc đoán làm chủ.",
        "director": "Trấn Thành", "actors": "Lê Giang, Uyển Ân, Song Luân, Trấn Thành, Khả Như",
        "duration": 102, "release": "2023-01-22", "end": "2023-03-30", "country": "Việt Nam",
        "rating": "T16", "status": "NgungChieu", "poster_file": "poster_nha_ba_nu.jpg",
        "trailer": "https://www.youtube.com/watch?v=nhabanu_trailer", "bg_color": (180, 80, 20)
    },
    {
        "id": 5, "title": "Bố Già", "english_title": "DAD, I'M SORRY",
        "desc": "Câu chuyện tình cha con đầy xúc động của Ba Sang - người đàn ông luôn lo chuyện bao đồng và con trai Quắn - YouTuber hiện đại.",
        "director": "Vũ Ngọc Đãng, Trấn Thành", "actors": "Trấn Thành, Tuấn Trần, Ngân Chi, NSND Ngọc Giàu",
        "duration": 128, "release": "2021-03-05", "end": "2021-05-15", "country": "Việt Nam",
        "rating": "P", "status": "NgungChieu", "poster_file": "poster_bo_gia.jpg",
        "trailer": "https://www.youtube.com/watch?v=bogia_trailer", "bg_color": (60, 70, 90)
    },
    {
        "id": 6, "title": "Mắt Biếc", "english_title": "DREAMY EYES",
        "desc": "Mối tình đơn phương da diết của Ngạn dành cho cô bạn thân Hà Lan với đôi mắt biếc biếc qua bao thăng trầm tuổi thanh xuân.",
        "director": "Victor Vũ", "actors": "Trần Nghĩa, Trúc Anh, Trần Phong, Đỗ Khánh Vân",
        "duration": 117, "release": "2019-12-20", "end": "2020-02-28", "country": "Việt Nam",
        "rating": "P", "status": "NgungChieu", "poster_file": "poster_mat_biec.jpg",
        "trailer": "https://www.youtube.com/watch?v=matbiec_trailer", "bg_color": (20, 70, 60)
    },
    {
        "id": 7, "title": "Hai Phượng", "english_title": "FURIE",
        "desc": "Hành trình nghẹt thở của một nữ giang hồ quy ẩn truy đuổi băng nhóm buôn bán trẻ em để giải cứu con gái bị bắt cóc.",
        "director": "Lê Văn Kiệt", "actors": "Ngô Thanh Vân, Cát Vy, Phan Thanh Nhiên, Phạm Anh Khoa",
        "duration": 98, "release": "2019-02-22", "end": "2019-04-30", "country": "Việt Nam",
        "rating": "T18", "status": "NgungChieu", "poster_file": "poster_hai_phuong.jpg",
        "trailer": "https://www.youtube.com/watch?v=haiphuong_trailer", "bg_color": (160, 20, 30)
    },
    {
        "id": 8, "title": "Em Chưa 18", "english_title": "JAILBAIT",
        "desc": "Câu chuyện tình yêu hài hước, dở khóc dở cười giữa tay chơi Hoàng hào hoa và cô nàng học sinh cá tính Linh Đan.",
        "director": "Lê Thanh Sơn", "actors": "Kaity Nguyễn, Kiều Minh Tuấn, Will, Châu Bùi",
        "duration": 95, "release": "2017-04-28", "end": "2017-06-30", "country": "Việt Nam",
        "rating": "T16", "status": "NgungChieu", "poster_file": "poster_em_chua_18.jpg",
        "trailer": "https://www.youtube.com/watch?v=emchua18_trailer", "bg_color": (190, 40, 100)
    },
    {
        "id": 9, "title": "Quỷ Cẩu", "english_title": "THE DOG SPIRIT",
        "desc": "Lấy cảm hứng từ truyền thuyết linh dị dân gian Chó đội nón mê, kể về những tai ương giáng xuống một gia đình làm nghề mổ chó.",
        "director": "Lưu Thành Luân", "actors": "Quang Tuấn, Nam Thư, Kim Xuân, Vân Dung, Quốc Quân",
        "duration": 108, "release": "2023-12-29", "end": "2024-03-01", "country": "Việt Nam",
        "rating": "T18", "status": "NgungChieu", "poster_file": "poster_quy_cau.jpg",
        "trailer": "https://www.youtube.com/watch?v=quycau_trailer", "bg_color": (40, 10, 10)
    },
    {
        "id": 10, "title": "Kẻ Ăn Hồn", "english_title": "THE SOUL REAPER",
        "desc": "Cổ thuật Rượu Sọ Người reo rắc kinh hoàng lên Làng Địa Ngục cổ xưa, hé lộ nguồn gốc lời nguyền gia tộc đẫm máu.",
        "director": "Trần Hữu Tấn", "actors": "Hoàng Hà, Võ Điền Gia Huy, Huỳnh Thanh Trực, Lan Phương",
        "duration": 109, "release": "2023-12-15", "end": "2024-02-15", "country": "Việt Nam",
        "rating": "T18", "status": "NgungChieu", "poster_file": "poster_ke_an_hon.jpg",
        "trailer": "https://www.youtube.com/watch?v=keanhon_trailer", "bg_color": (30, 20, 40)
    },
    {
        "id": 11, "title": "Dune: Hành Tinh Cát 2", "english_title": "DUNE: PART TWO",
        "desc": "Paul Atreides hợp lực cùng Chani và tộc người Fremen để trả thù những kẻ đã hủy hoại gia đình mình và định đoạt số phận vũ trụ.",
        "director": "Denis Villeneuve", "actors": "Timothée Chalamet, Zendaya, Rebecca Ferguson, Javier Bardem",
        "duration": 166, "release": "2024-03-01", "end": "2024-05-15", "country": "Mỹ",
        "rating": "T16", "status": "DangChieu", "poster_file": "poster_dune_2.jpg",
        "trailer": "https://www.youtube.com/watch?v=dune2_trailer", "bg_color": (160, 100, 30)
    },
    {
        "id": 12, "title": "Oppenheimer", "english_title": "OPPENHEIMER",
        "desc": "Tác phẩm tiểu sử lịch sử khắc họa cuộc đời của J. Robert Oppenheimer, cha đẻ của bom nguyên tử trong Dự án Manhattan.",
        "director": "Christopher Nolan", "actors": "Cillian Murphy, Emily Blunt, Matt Damon, Robert Downey Jr.",
        "duration": 180, "release": "2023-08-11", "end": "2023-11-10", "country": "Mỹ",
        "rating": "T18", "status": "NgungChieu", "poster_file": "poster_oppenheimer.jpg",
        "trailer": "https://www.youtube.com/watch?v=oppenheimer_trailer", "bg_color": (180, 60, 20)
    },
    {
        "id": 13, "title": "Godzilla x Kong: Đế Chế Mới", "english_title": "THE NEW EMPIRE",
        "desc": "Hai quái thú khổng lồ Godzilla và Kong phải hợp sức đối đầu với một mối đe dọa sinh tử chưa từng được biết đến từ Trái Đất Rỗng.",
        "director": "Adam Wingard", "actors": "Rebecca Hall, Brian Tyree Henry, Dan Stevens, Kaylee Hottle",
        "duration": 115, "release": "2024-03-29", "end": "2024-05-30", "country": "Mỹ",
        "rating": "T13", "status": "DangChieu", "poster_file": "poster_godzilla_kong.jpg",
        "trailer": "https://www.youtube.com/watch?v=godzillakong_trailer", "bg_color": (20, 60, 120)
    },
    {
        "id": 14, "title": "Kung Fu Panda 4", "english_title": "KUNG FU PANDA 4",
        "desc": "Po chuẩn bị trở thành Thủ lĩnh Tinh thần của Thung lũng Bình Yên và phải tìm kiếm một Thần Long Đại Hiệp mới để kế vị.",
        "director": "Mike Mitchell", "actors": "Jack Black, Awkwafina, Viola Davis, Dustin Hoffman",
        "duration": 94, "release": "2024-03-08", "end": "2024-05-20", "country": "Mỹ",
        "rating": "P", "status": "DangChieu", "poster_file": "poster_kungfu_panda_4.jpg",
        "trailer": "https://www.youtube.com/watch?v=kungfupanda4_trailer", "bg_color": (30, 120, 60)
    },
    {
        "id": 15, "title": "Thám Tử Lừng Danh Conan: Ngôi Sao 5 Cánh 1 Triệu Đô", "english_title": "THE MILLION-DOLLAR PENTAGRAM",
        "desc": "Conan, Heiji Hattori và Siêu trộm Kaito Kid bước vào cuộc truy tìm kho báu bí ẩn liên quan đến thanh kiếm Nhật cổ tại Hakodate.",
        "director": "Chika Nagaoka", "actors": "Minami Takayama, Wakana Yamazaki, Kappei Yamaguchi, Ryo Horikawa",
        "duration": 111, "release": "2024-08-02", "end": "2024-10-15", "country": "Nhật Bản",
        "rating": "K", "status": "DangChieu", "poster_file": "poster_conan_ngoi_sao.jpg",
        "trailer": "https://www.youtube.com/watch?v=conan_trailer", "bg_color": (25, 40, 110)
    },
    {
        "id": 16, "title": "Doraemon: Bản Giao Hưởng Địa Cầu", "english_title": "NOBITA'S EARTH SYMPHONY",
        "desc": "Doraemon, Nobita và nhóm bạn bắt đầu chuyến phiêu lưu âm nhạc kỳ thú để giải cứu Trái Đất khỏi một sinh vật đe dọa nuốt chửng âm thanh.",
        "director": "Kazuaki Imai", "actors": "Wasabi Mizuta, Megumi Ohara, Yumi Kakazu, Subaru Kimura",
        "duration": 115, "release": "2024-05-24", "end": "2024-07-31", "country": "Nhật Bản",
        "rating": "P", "status": "DangChieu", "poster_file": "poster_doraemon_giao_huong.jpg",
        "trailer": "https://www.youtube.com/watch?v=doraemon_trailer", "bg_color": (20, 100, 180)
    },
    {
        "id": 17, "title": "Avatar: Dòng Chảy Của Nước", "english_title": "THE WAY OF WATER",
        "desc": "Gia đình Jake Sully và Neytiri phải rời khỏi khu rừng thân thuộc và hòa nhập với bộ tộc Metkayina vùng đại dương trên hành tinh Pandora.",
        "director": "James Cameron", "actors": "Sam Worthington, Zoe Saldana, Sigourney Weaver, Stephen Lang",
        "duration": 192, "release": "2022-12-16", "end": "2023-03-15", "country": "Mỹ",
        "rating": "T13", "status": "NgungChieu", "poster_file": "poster_avatar_2.jpg",
        "trailer": "https://www.youtube.com/watch?v=avatar2_trailer", "bg_color": (10, 80, 140)
    },
    {
        "id": 18, "title": "Spider-Man: Không Còn Nhà", "english_title": "NO WAY HOME",
        "desc": "Danh tính Peter Parker bị lộ dẫn đến việc cậu nhờ Doctor Strange can thiệp ma thuật, vô tình mở ra hiểm họa đa vũ trụ.",
        "director": "Jon Watts", "actors": "Tom Holland, Zendaya, Benedict Cumberbatch, Willem Dafoe",
        "duration": 148, "release": "2021-12-17", "end": "2022-03-15", "country": "Mỹ",
        "rating": "T13", "status": "NgungChieu", "poster_file": "poster_spiderman_no_way_home.jpg",
        "trailer": "https://www.youtube.com/watch?v=spiderman_trailer", "bg_color": (150, 20, 30)
    },
    {
        "id": 19, "title": "Avengers: Hồi Kết", "english_title": "AVENGERS: ENDGAME",
        "desc": "Sau cái búng tay hủy diệt của Thanos, các Avengers sống sót tập hợp lại để thực hiện canh bạc du hành thời gian nhằm cứu vũ trụ.",
        "director": "Anthony Russo, Joe Russo", "actors": "Robert Downey Jr., Chris Evans, Mark Ruffalo, Chris Hemsworth",
        "duration": 181, "release": "2019-04-26", "end": "2019-07-15", "country": "Mỹ",
        "rating": "T13", "status": "NgungChieu", "poster_file": "poster_avengers_endgame.jpg",
        "trailer": "https://www.youtube.com/watch?v=endgame_trailer", "bg_color": (70, 25, 90)
    },
    {
        "id": 20, "title": "Hố Đen Tử Thần", "english_title": "INTERSTELLAR",
        "desc": "Nhóm nhà khoa học dẫn đầu bởi Cooper thực hiện chuyến du hành vượt ra khỏi dải ngân hà để tìm kiếm hành tinh sống mới cho nhân loại.",
        "director": "Christopher Nolan", "actors": "Matthew McConaughey, Anne Hathaway, Jessica Chastain, Michael Caine",
        "duration": 169, "release": "2014-11-07", "end": "2015-02-15", "country": "Mỹ",
        "rating": "T13", "status": "NgungChieu", "poster_file": "poster_interstellar.jpg",
        "trailer": "https://www.youtube.com/watch?v=interstellar_trailer", "bg_color": (20, 30, 50)
    },
    {
        "id": 21, "title": "Những Mảnh Ghép Cảm Xúc 2", "english_title": "INSIDE OUT 2",
        "desc": "Riley bước vào tuổi dậy thì với những cảm xúc mới xuất hiện tại trung tâm điều khiển: Lo Âu, Ganh Tị, Xấu Hổ và Chán Nản.",
        "director": "Kelsey Mann", "actors": "Amy Poehler, Maya Hawke, Phyllis Smith, Lewis Black",
        "duration": 96, "release": "2024-06-14", "end": "2024-08-31", "country": "Mỹ",
        "rating": "P", "status": "DangChieu", "poster_file": "poster_inside_out_2.jpg",
        "trailer": "https://www.youtube.com/watch?v=insideout2_trailer", "bg_color": (180, 90, 20)
    },
    {
        "id": 22, "title": "Kẻ Trộm Mặt Trăng 4", "english_title": "DESPICABLE ME 4",
        "desc": "Gru, Lucy và các cô con gái chào đón thành viên mới Gru Jr., đồng thời phải trốn chạy kẻ thù truyền kiếp Maxime Le Mal vượt ngục.",
        "director": "Chris Renaud", "actors": "Steve Carell, Kristen Wiig, Will Ferrell, Sofia Vergara",
        "duration": 94, "release": "2024-07-05", "end": "2024-09-15", "country": "Mỹ",
        "rating": "P", "status": "DangChieu", "poster_file": "poster_despicable_me_4.jpg",
        "trailer": "https://www.youtube.com/watch?v=despicableme4_trailer", "bg_color": (220, 160, 20)
    },
    {
        "id": 23, "title": "Deadpool & Wolverine", "english_title": "DEADPOOL & WOLVERINE",
        "desc": "Wade Wilson tái xuất khi tổ chức TVA lôi kéo anh vào một nhiệm vụ bảo vệ dòng thời gian cùng biến thể Logan đầy miễn cưỡng.",
        "director": "Shawn Levy", "actors": "Ryan Reynolds, Hugh Jackman, Emma Corrin, Matthew Macfadyen",
        "duration": 128, "release": "2024-07-26", "end": "2024-10-15", "country": "Mỹ",
        "rating": "T18", "status": "DangChieu", "poster_file": "poster_deadpool_wolverine.jpg",
        "trailer": "https://www.youtube.com/watch?v=deadpool_trailer", "bg_color": (160, 20, 20)
    },
    {
        "id": 24, "title": "Quật Mộ Trùng Ma", "english_title": "EXHUMA",
        "desc": "Hai pháp sư, một thầy phong thủy và một chuyên gia mai táng nhận lời khai quật một ngôi mộ bí ẩn tại vùng núi hẻo lánh và giải phóng ác linh.",
        "director": "Jang Jae-hyun", "actors": "Choi Min-sik, Kim Go-eun, Yoo Hae-jin, Lee Do-hyun",
        "duration": 134, "release": "2024-03-15", "end": "2024-05-30", "country": "Hàn Quốc",
        "rating": "T16", "status": "DangChieu", "poster_file": "poster_exhuma.jpg",
        "trailer": "https://www.youtube.com/watch?v=exhuma_trailer", "bg_color": (40, 45, 35)
    },
    {
        "id": 25, "title": "Gặp Lại Chị Bầu", "english_title": "MEET MY PREGNANT SISTER",
        "desc": "Phúc bất ngờ xuyên không về Sài Gòn thập niên 1990 và gặp gỡ nhóm bạn trẻ nhiệt huyết cùng Huyền - người mang thai đầy bí ẩn.",
        "director": "Đoàn Nhất Trung", "actors": "Anh Tú, Diệu Nhi, Ngọc Phước, Quốc Khánh, Lê Giang",
        "duration": 114, "release": "2024-02-10", "end": "2024-04-10", "country": "Việt Nam",
        "rating": "T13", "status": "NgungChieu", "poster_file": "poster_gap_lai_chi_bau.jpg",
        "trailer": "https://www.youtube.com/watch?v=gaplaichibau_trailer", "bg_color": (170, 70, 110)
    },
    {
        "id": 26, "title": "Mufasa: Vua Sư Tử", "english_title": "MUFASA: THE LION KING",
        "desc": "Hành trình từ chú sư tử mồ côi đơn độc vươn lên thành một vị vua huyền thoại của Vùng Đất Kiêu Hãnh cùng Taka.",
        "director": "Barry Jenkins", "actors": "Aaron Pierre, Kelvin Harrison Jr., Seth Rogen, Billy Eichner",
        "duration": 118, "release": "2024-12-20", "end": "2025-02-28", "country": "Mỹ",
        "rating": "P", "status": "SapChieu", "poster_file": "poster_mufasa.jpg",
        "trailer": "https://www.youtube.com/watch?v=mufasa_trailer", "bg_color": (160, 90, 20)
    }
]

# Tạo ảnh poster
font_path = "C:/Windows/Fonts/arial.ttf"
try:
    font_large = ImageFont.truetype(font_path, 28)
    font_medium = ImageFont.truetype(font_path, 18)
    font_small = ImageFont.truetype(font_path, 14)
    font_badge = ImageFont.truetype(font_path, 16)
except Exception:
    font_large = ImageFont.load_default()
    font_medium = font_large
    font_small = font_large
    font_badge = font_large

for m in movies_info:
    width, height = 400, 600
    img = Image.new("RGB", (width, height), color=(15, 18, 25))
    draw = ImageDraw.Draw(img)

    # Gradient background
    r_base, g_base, b_base = m["bg_color"]
    for y in range(height):
        factor = 1.0 - (y / height) * 0.7
        r = int(r_base * factor)
        g = int(g_base * factor)
        b = int(b_base * factor)
        draw.line([(0, y), (width, y)], fill=(r, g, b))

    # Dark overlay bottom
    for y in range(350, height):
        alpha = (y - 350) / (height - 350)
        draw.line([(0, y), (width, y)], fill=(int(10 * (1 - alpha)), int(12 * (1 - alpha)), int(18 * (1 - alpha))))

    # Border
    draw.rectangle([10, 10, width - 10, height - 10], outline=(220, 200, 140), width=2)
    draw.rectangle([14, 14, width - 14, height - 14], outline=(100, 90, 70), width=1)

    # Cinema watermark
    draw.text((width // 2, 60), "CINEMA POSTER", fill=(200, 200, 220), font=font_small, anchor="mm")
    draw.line([(width // 2 - 80, 75), (width // 2 + 80, 75)], fill=(220, 200, 140), width=1)

    # Age Rating Badge (top right)
    badge_text = m["rating"]
    draw.rectangle([width - 80, 25, width - 25, 55], fill=(220, 40, 40), outline=(255, 255, 255), width=1)
    draw.text((width - 52, 40), badge_text, fill=(255, 255, 255), font=font_badge, anchor="mm")

    # Center Visual Element (Camera / Star / Circle)
    draw.ellipse([width // 2 - 60, 140, width // 2 + 60, 260], outline=(240, 210, 120), width=3)
    draw.text((width // 2, 200), f"★ {m['country']} ★", fill=(255, 240, 180), font=font_medium, anchor="mm")

    # Vietnamese Title (wrapped)
    title = m["title"]
    if len(title) > 20:
        words = title.split(" ")
        mid = len(words) // 2
        line1 = " ".join(words[:mid])
        line2 = " ".join(words[mid:])
        draw.text((width // 2, 360), line1, fill=(255, 255, 255), font=font_large, anchor="mm")
        draw.text((width // 2, 395), line2, fill=(255, 255, 255), font=font_large, anchor="mm")
    else:
        draw.text((width // 2, 375), title, fill=(255, 255, 255), font=font_large, anchor="mm")

    # English Title
    draw.text((width // 2, 430), m["english_title"], fill=(220, 200, 130), font=font_small, anchor="mm")

    # Director & Duration Info
    draw.line([(30, 455), (width - 30, 455)], fill=(120, 120, 130), width=1)
    draw.text((width // 2, 480), f"Đạo diễn: {m['director']}", fill=(210, 210, 220), font=font_small, anchor="mm")
    draw.text((width // 2, 510), f"Thời lượng: {m['duration']} phút  |  Trạng thái: {m['status']}", fill=(180, 180, 190), font=font_small, anchor="mm")
    draw.text((width // 2, 545), f"Khởi chiếu: {m['release']}", fill=(240, 200, 100), font=font_medium, anchor="mm")

    # Save to Ảnh/
    file_path = os.path.join(image_dir, m["poster_file"])
    img.save(file_path, "JPEG", quality=90)
    print(f"Đã tạo poster: {m['poster_file']}")

print("Hoàn tất tạo 26 poster trong thư mục Ảnh!")

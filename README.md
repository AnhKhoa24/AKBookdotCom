# 📖 AKBookDotCom - Hướng dẫn chạy dự án với Docker

## 🛠 Yêu cầu hệ thống
- **Docker**: Cài đặt Docker Desktop [tại đây](https://www.docker.com/get-started/)
- **Docker Compose**: Đã bao gồm trong Docker Desktop

## 📂 Cấu trúc dự án
```plaintext
AKBookDotCom/
│── docker-compose.yml
│── Dockerfile
│── appsettings.json
│── appsettings.Development.json
│── Program.cs
│── ...
```

## 🚀 Cách chạy dự án
### 🏗️ 1. Build và chạy container
Mở terminal tại thư mục dự án và chạy lệnh sau:
```sh
docker-compose up --build
```
Lệnh này sẽ:
✅ Build Docker image
✅ Tạo và chạy các container cần thiết

### 🔄 2. Dừng container
Nếu muốn dừng toàn bộ container, chạy:
```sh
docker-compose down
```

### 🔥 3. Chạy lại container mà không build lại
```sh
docker-compose up
```

## 🌐 Truy cập ứng dụng
Nếu bạn chạy với cổng mặc định, mở trình duyệt và truy cập:
```
http://localhost:5000
```
hoặc (nếu dùng HTTPS)
```
https://localhost:702
```

## 🛠 Gỡ lỗi và logs
- Xem log container:
  ```sh
  docker logs -f <container_id>
  ```
- Xem danh sách container đang chạy:
  ```sh
  docker ps
  ```
- Kiểm tra container đã dừng:
  ```sh
  docker ps -a
  ```

## ⚙️ Cấu hình môi trường
Bạn có thể chỉnh sửa **appsettings.json** hoặc tạo file **.env** để truyền biến môi trường vào Docker.

---
🚀 **Chúc bạn thành công!**


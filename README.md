# MiniShop

## Mô tả

MiniShop là một ứng dụng web sử dụng .NET Clean Architecture và Angular để phát triển frontend.

## Yêu cầu hệ thống

- .NET SDK 8.0 trở lên
- Node.js 18.x trở lên
- Angular CLI 18.x trở lên
- Git

## Cài đặt

### Clone Repository

1. Clone repository từ GitHub:

```sh
git clone https://github.com/imkhoanguyen/mini-shop.git
```

2. Cài đặt Backend:
- Chuyển đến thư mục backend và cài đặt các package cần thiết:
```sh
cd Shop.API
dotnet restore
```
- Chạy lệnh build để đảm bảo mọi thứ hoàn hảo:
```sh
dotnet buid
```
- Chạy ứng dụng:
```
dotnet watch run hoặc dotnet run (ứng dụng sẽ chạy tại http://localhost:5000)
```
3. Cài đặt FrontEnd:
- Chuyển đến thư mục Frontend và cài đặt các package cần thiết:
```sh
cd Client
npm install
```
- Chạy ứng dụng Angular:
```sh
ng serve (ứng dụng sẽ chạy tại http://localhost:4200)
```
## Cấu trúc dự án

### Backend (Shop.API)
Clean Architecture:
Mô hình phân lớp rõ ràng với các lớp Domain, Application, Infrastructure, và Presentation.

> Công nghệ sử dụng:

* .NET 8.0
* Entity Framework Core cho quản lý cơ sở dữ liệu
* Redis
* SignalR
* Swagger 
### Frontend (Client)
Angular Framework:
Ứng dụng SPA hiện đại với giao diện người dùng thân thiện.

> Công nghệ sử dụng:

* Angular 18.x
* CSS/Bootstrap cho thiết kế giao diện (tùy chỉnh thêm).
* PrimeNG cho các thành phần UI.

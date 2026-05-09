# 🏥 MiniSIMRS API (Sistem Informasi Manajemen Rumah Sakit)

MiniSIMRS adalah RESTful API yang dirancang untuk mengelola data inti Rumah Sakit, termasuk manajemen Pasien, Dokter, dan Rekam Medis. Proyek ini dibangun dengan mengedepankan arsitektur yang bersih (*Clean Architecture* via *Service Layer*), keamanan akses data, dan kemudahan skalabilitas.

## 🚀 Fitur Utama

*   **Autentikasi & Otorisasi Aman:** Menggunakan **JSON Web Tokens (JWT)** untuk mengamankan *endpoint*. Implementasi *Role-Based Access Control* (RBAC) untuk membedakan hak akses (misal: Admin, Dokter).
*   **Arsitektur Service Layer:** Pemisahan *logic* bisnis dari *Controller* ke *Service Layer* untuk memudahkan *maintenance* dan *Unit Testing*.
*   **Soft Delete & Restore:** Menerapkan konsep *Soft Delete* pada rekam medis (data tidak dihapus permanen dari *database*, melainkan ditandai, dan dapat di-*restore* kembali).
*   **Manajemen Relasional Data:** Relasi antar tabel yang solid menggunakan Entity Framework Core (One-to-Many).
*   **Interactive API Documentation:** Dilengkapi dengan antarmuka Swagger UI yang sudah terintegrasi dengan mekanisme *Bearer Token*.

## 🛠️ Tech Stack

*   **Framework:** .NET 10 / ASP.NET Core Web API
*   **Language:** C#
*   **ORM:** Entity Framework (EF) Core
*   **Database:** SQLite (Dapat dengan mudah dimigrasikan ke SQL Server, MySQL, atau PostgreSQL)
*   **Security:** Microsoft.AspNetCore.Authentication.JwtBearer
*   **API Documentation:** Swashbuckle.AspNetCore (Swagger)

## 📁 Struktur Proyek

```text
MiniSIMRS.Api/
├── Controllers/       # Menangani request HTTP dan routing
├── DTOs/              # Data Transfer Objects untuk validasi input/output API
├── Models/            # Entitas database / Struktur tabel
├── Services/          # Logika bisnis utama (AuthService, RekamMedisService, dll)
├── Data/              # AppDbContext dan konfigurasi Entity Framework
├── appsettings.json   # Konfigurasi aplikasi (Database connection, JWT Secret)
└── Program.cs         # Entry point, Dependency Injection, dan Middleware
```
## ⚙️ Persyaratan Sistem (Prerequisites)
Pastikan sistem Anda sudah terinstal:

* **.NET SDK 10.0** (atau versi yang sesuai dengan proyek)

* **Visual Studio Code** atau Visual Studio 2022

* **Tools SQLite** (Opsional, seperti DB Browser for SQLite untuk melihat isi database)

## 🚦 Cara Menjalankan Proyek (Getting Started)
### 1. Clone Repositori
```bash
git clone <url-repositori-anda>
cd MiniSIMRS.Api
```
### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Terapkan Migrasi Database
Perintah ini akan membuat file MiniSIMRS.db beserta seluruh tabelnya berdasarkan konfigurasi Models terbaru.
```bash
dotnet ef database update
```

### 4. Jalankan Aplikasi
```bash
dotnet run
```

Aplikasi akan berjalan di http://localhost<port>. 

Buka browser dan akses http://localhost:/swagger <port> untuk melihat dan mencoba API secara interaktif.

## 🔒 Panduan Penggunaan API (Authentication Flow)

Karena API ini diamankan dengan JWT, ikuti langkah berikut untuk mencoba endpoint yang terkunci:

1.  Akses endpoint POST /api/Auth/register untuk membuat akun baru. Masukkan Username, dan Password.
2.  Akses endpoint POST /api/Auth/login dengan kredensial yang baru saja dibuat.
3.  Salin token teks panjang (eyJ...) dari response body.
4.  Klik tombol "Authorize" (ikon gembok) di kanan atas Swagger UI.
5.  Ketik "Bearer <spasi> <token-anda>" lalu klik Authorize.
6.  Anda sekarang memiliki akses penuh ke endpoint yang diproteksi!

## 📡 Daftar Endpoint Utama

| Method | Endpoint | Deskripsi | Akses |
| :--- | :--- | :--- | :--- |
| POST | /api/Auth/register | Mendaftarkan user baru | Publik |
| POST | /api/Auth/login | Mendapatkan token JWT | Publik |
| GET | /api/RekamMedis/{pasienId} | Mengambil seluruh riwayat medis pasien | Terkunci (JWT) |
| POST | /api/RekamMedis | Menambahkan data rekam medis baru | Terkunci (Role: Dokter) |
| PUT | /api/RekamMedis/{id} | Memperbarui data rekam medis | Terkunci (Role: Dokter) |
| DELETE| /api/RekamMedis/{id} | Menghapus (Soft Delete) rekam medis | Terkunci (Role: Admin/Dokter)|
| PUT | /api/RekamMedis/{id}/restore| Mengembalikan data rekam medis terhapus | Terkunci (Role: Admin) |

*(Catatan: Modul Pasien dan Dokter memiliki endpoint CRUD standar masing-masing).*

## 💡 Highlight Pengembangan

Proyek ini dibangun dengan pola Data Transfer Object (DTO) untuk mencegah Over-Posting dan Under-Posting data. Selain itu, logika manipulasi database disembunyikan dari Controller untuk memastikan pengujian kode (mocking) di masa depan dapat dilakukan dengan sangat mudah.
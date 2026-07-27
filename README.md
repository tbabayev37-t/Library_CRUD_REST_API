# Library Management REST API - Week 2 (Authentication & Authorization)

Bu layihə kitabxana idarəetmə sistemi üçün təhlükəsizlik, istifadəçi autentifikasiyası və rol əsaslı avtorizasiya mexanizmlərinin tətbiq olunduğu **ASP.NET Core RESTful API** mərhələsidir.

## 🛠 Texnologiyalar və Kitabxanalar

* **Authentication & Authorization:** JWT (JSON Web Tokens), ASP.NET Core Bearer Authentication
* **Password Hashing:** BCrypt.Net-Next
* **Documentation:** Swagger UI / OpenAPI (Bearer Token dəstəyi ilə)
* **Architecture:** N-Tier Architecture (API, Business, Core, DataAccess)
* **ORM & Database:** Entity Framework Core & SQL Server
* **Mapper:** AutoMapper

## 📌 Həftə 2 Xüsusiyyətləri

* **User Entity & Secure Password Hashing:** İstifadəçi məlumatlarının idarə olunması və şifrələrin bazada heç bir halda plain-text saxlanılmadan BCrypt alqoritmi vasitəsilə hash-lənməsi.
* **JWT Authentication:** `Register` və `Login` endpoint-ləri vasitəsilə təhlükəsiz giriş və sistemə sorğu göndərmək üçün JWT Access Token verilməsi.
* **Role-Based Access Control (RBAC):** `User` və `Admin` rollarına əsasən resurslara girişin məhdudlaşdırılması (`[Authorize(Roles = "Admin")]`).
* **Proper Status Codes Handling:** Anonim sorğular üçün `401 Unauthorized`, kifayət qədər rolu olmayan istifadəçilər üçün `403 Forbidden` status kodlarının qaytarılması.
* **Token Expiration & Validation:** JWT Token-ə müəyyən ömür təyin olunması və `ValidateLifetime` vasitəsilə vaxtı bitmiş tokenlərin avtomatik bloklanması.

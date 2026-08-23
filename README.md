# 📚 Library Management System — RESTful Web API

A robust, enterprise-grade **Library Management System Web API** built with **C#** and **.NET Core**. Designed following **N-Layer (Clean) Architecture** principles, this production-ready backend project incorporates advanced concepts including authentication, dynamic querying, performance optimization, and automated testing.

---

## 🚀 Key Features & Implementation Highlights

### 🏛️ Architecture & Clean Code
* **N-Layer Architecture:** Divided into strict logical layers (`Core`, `DataAccess`, `Business`, `API`, `Tests`) for high maintainability and testability.
* **OOP & SOLID Principles:** Implemented repository patterns, interfaces, and clean code practices.

### 🔐 Security & Authorization
* **Custom User Authentication:** Secure password hashing mechanisms for user management.
* **JWT (JSON Web Token):** Token-based authentication and role-based access control (RBAC) to secure API endpoints with lifespan management.

### 📊 Database & ORM Operations
* **Entity Framework Core:** Relational database mapping with **One-to-Many** and **Many-to-Many** entity design.
* **Advanced Querying & Performance:**
  * Dynamic filtering, sorting, and pagination for list endpoints.
  * **N+1 Query Optimization:** Optimized LINQ queries with eager loading (`Include` / `ThenInclude`) to prevent database bottlenecks.
  * **ACID Transactions:** Thread-safe multi-table write operations.

### ⚡ Performance, Caching & Background Operations
* **Caching & Invalidation:** In-memory caching for high-frequency reads with automatic cache invalidation upon data updates.
* **Asynchronous Processing:** `async/await` patterns across all service layers for non-blocking I/O operations.
* **Background Tasks:** Scheduled jobs for automated background processing.

### 🧪 Quality Assurance & API Documentation
* **Unit Testing:** Comprehensive test coverage for service and logic layers using **xUnit** and **Moq**.
* **Input Validation & Error Handling:** Fluent validation pipelines with clean JSON error response formats.
* **Swagger / OpenAPI:** Interactive API documentation and endpoint testing UI.

---

## 🛠️ Tech Stack & Tools

* **Language:** C# 12 / .NET Core
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core
* **Security:** JWT Authentication, BCrypt Password Hashing
* **Testing:** xUnit, Moq
* **Documentation:** Swagger UI (Swashbuckle)
* **DevOps & Containerization:** Dockerized deployment setup

---

## 📂 Project Structure

```text
Library_CRUD_REST_API/
├── CRUD_REST_API/             # Web API Layer (Controllers, Middlewares, Program.cs)
├── CRUD_REST_API.Business/    # Business Logic, DTOs, Validations, Mappings
├── CRUD_REST_API.Core/        # Domain Entities, Interfaces, Base Infrastructure
├── CRUD_REST_API.DataAccess/  # DB Context, Migrations, Repository Implementations
└── CRUD_REST_API.Tests/       # Unit & Integration Tests (xUnit)

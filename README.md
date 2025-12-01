# BloodLink 🩸❤️
**Blood Bank Management System**

BloodLink is a system to manage the full lifecycle of blood donation: donor registration, donation records, stock control and report generation. The goal is to simplify donor registration and donations, and provide queries and reports useful for blood bank management.

The project was initially developed as a backend API in ASP.NET Core and follows Clean Architecture principles and best practices in the .NET ecosystem.

---

## 🚀 Project Overview

BloodLink aims to:
- Simplify donor registration and donation recording;
- Enforce clinical and eligibility rules for donation;
- Automatically update blood stock after donations;
- Generate management reports on availability and donation activity;
- Integrate with external services (e.g., address lookup via CEP).

Project phases:
1. **Backend API** (ASP.NET Core / .NET 8) — core functionality implemented.
2. **Frontend Interface** — to be developed in subsequent phases (optional).

---

## 🛠️ Features

### Registration and Validation
- Donor registration with validation (prevents duplicate emails).
- Integration with an external API for address lookup by CEP.
- Clinical validations: age, weight, donation frequency, and donated volume.

### Donations and Stock
- Register donations and automatically update the stock.
- Stock control by blood type and Rh factor.
- Notifications/alerts when stock for a specific type/factor reaches a configured minimum.

### Queries and Reports
- Query donors and view donation history.
- Report of total available blood volume by type/Rh.
- Report of donations in the last 30 days including donor information.

---

## ✅ Key Business Rules

- Do not allow registering a donor with the same email.
- Minors may be registered but cannot donate.
- Minimum weight to donate: 50 kg.
- Donation frequency:
  - Females: allowed only every 90 days.
  - Males: allowed only every 60 days.
- Donation volume must be between 420 ml and 470 ml.
- When registering a donation, stock must be updated automatically.
- Emit an alert when stock reaches the configured minimum.

---

## 🧱 Domain Model

- Donor
  - Id (int)
  - FullName (string)
  - Email (string)
  - BirthDate (DateTime)
  - Gender (string)
  - Weight (double)
  - BloodType (string)
  - RhFactor (string)
  - Donations (List<Donation>)
  - Address (Address) (PLUS)

- Address
  - Id (int)
  - Street (string)
  - City (string)
  - State (string)
  - ZipCode (string)
  - Donor (Donor)

- Donation
  - Id (int)
  - DonorId (int)
  - DonationDate (DateTime)
  - QuantityML (int)
  - Donor (Donor)

- BloodStock
  - Id (int)
  - BloodType (string)
  - RhFactor (string)
  - QuantityML (int)

---

## 🧰 Technologies and Patterns

- .NET 8 (ASP.NET Core)
- C#
- Entity Framework Core (EF Core)
- Domain-Driven Design (DDD)
- CQRS (Command Query Responsibility Segregation)
- MediatR (command/query/event mediation)
- FluentValidation (input validation)
- Repository Pattern
- Unit of Work (UoW)
- Middlewares (exception handling, logging)
- Clean Architecture
- IEntityTypeConfiguration for entity mapping
- Swagger (interactive API documentation)
- HostedServices and Domain Events (advanced)
- Supported storage options: In-Memory, SQLite, SQL Server
- Unit testing (planned/implemented per layer)

---

## ⚙️ How to run locally (generic)

1. Clone the repository:
   git clone https://github.com/hiltonsouza/BloodLink.git

2. Enter the project folder and run the API:
   cd BloodLink/BloodLink.API
   dotnet run

3. The API exposes Swagger UI in development (e.g.: https://localhost:5001/swagger).

Note: update the connection string in appsettings.json to use InMemory / SQLite / SQL Server as needed.

---

## 📈 Improvements / Roadmap

- Implement responsive frontend (web/mobile).
- Add authentication/authorization (JWT / Identity).
- Implement notifications (email / push) for stock alerts.
- Export reports (CSV/PDF).
- Integration and unit/integration tests with coverage.
- CI/CD (GitHub Actions) and containerization (Docker).
- Monitoring and observability (Application Insights / Prometheus + Grafana).

### Next prioritized feature
- Deploy and implement the system on **Azure** (App Service, Azure SQL / Azure Database for PostgreSQL, Azure Functions for background jobs or notifications, and Application Insights for monitoring).
- Refactor the codebase to:
  - Improve separation of concerns;
  - Reduce coupling;
  - Increase test coverage with unit and integration tests.
- Evaluate and apply advanced C# features selectively to improve code quality and performance:
  - Use of **generics** to reduce duplication and increase reuse;
  - Consider **structs** for small immutable value objects when appropriate;
  - Use **tuples (ValueTuple)** for concise internal returns (careful about clarity);
  - Explore **pattern matching**, **nullable reference types**, **Span/Memory<T>**, **records**, and other modern C# features as needed.
- Plan migration to Azure managed resources (backups, HA, scaling).

---

## 🤝 Contributing

Contributions are welcome! Feel free to:
- Open issues;
- Send pull requests;
- Propose improvements or corrections to business rules.

---

## 📄 License

MIT License

---

## ✨ Author

Developed by Hilton Souza — Email: h_iltonsouza@outlook.com | Linkedin: hilton-souza | Github: hiltonsouza

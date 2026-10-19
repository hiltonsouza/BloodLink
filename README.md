# BloodLink 🩸

BloodLink is a comprehensive blood bank management system designed to streamline the management of donors, donations, and blood stock inventory. Built with **.NET 8** and following **Clean Architecture** principles, it implements modern design patterns to ensure scalability, maintainability, and reliability.

## 🚀 Features

### 👥 Donor Management
- **Smart Registration**: Register donors with automated eligibility checks (age, weight, donation intervals).
- **Address Integration**: Automatic address lookup leveraging **ViaCEP integration**.
- **Donation History**: Track and manage the complete history of donations for each donor.

### 💉 Donation Management
- **Automated Workflows**: Register donations with automatic impact on blood stock inventory.
- **Reporting**: Generate detailed reports on recent donations and donor trends.
- **Validation**: Strict business rules validation using **FluentValidation** and **Domain Exceptions**.

### 📊 Inventory & Monitoring
- **Real-time Stock**: Track blood bags by type (A, B, AB, O) and Rh factor (+/-).
- **Proactive Monitoring**: Integrated **Background Services** that scan stock levels every 24 hours.
- **Smart Alerts**: Automated notifications for low stock levels to ensure blood bank readiness.

## 🏗️ Architecture & Patterns

The project is a showcase of **Clean Architecture** and robust software engineering patterns:

### 📐 Design Principles & Patterns
- **DDD (Domain-Driven Design)**: Focus on the core domain with Rich Entities, Aggregate Roots, and **Domain Events**.
- **CQRS (Command Query Responsibility Segregation)**: Clear separation between write and read operations implemented via **MediatR**.
- **SOLID Principles**: High adherence to SOLID for decoupled and testable code.
- **Repository & Unit of Work**: Abstracted data access layer ensuring transaction integrity.
- **Dependency Injection**: Native .NET DI container used throughout the entire solution.

### 🛠️ Technical Stack
- **Framework**: .NET 8 (Web API)
- **Database**: SQL Server with **Entity Framework Core**.
- **Validation**: **FluentValidation** for clean, declarative request validation.
- **Messaging**: **MediatR** for decoupled in-process messaging.
- **API Documentation**: **Swagger/OpenAPI** for interactive exploration.
- **Background Tasks**: **IHostedService / BackgroundService** for inventory monitoring.
- **Error Handling**: **Global Exception Handling** with **Problem Details** (RFC 7807).

## 📂 Project Structure

```bash
BloodLink/
├── BloodLink.API/           # Entry point, Controllers, Middlewares, Background Services
├── BloodLink.Application/   # Use Cases (Commands, Queries), Validators, DTOs
├── BloodLink.Core/          # Domain Layer (Entities, Rules, Domain Events, Repositories Interfaces)
├── BloodLink.Infrastructure/# Persistence (EF Core, SQL Server), External Integrations (ViaCEP)
└── BloodLink.UnitTests/     # Automated tests for Core and Application layers
```

## 🏁 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server

### Installation

1. **Clone & Setup**
   ```bash
   git clone https://github.com/your-username/BloodLink.git
   cd BloodLink
   ```

2. **Database Initialization**
   The project uses EF Core Migrations. To initialize the database:
   ```bash
   dotnet ef database update --project BloodLink.Infrastructure --startup-project BloodLink.API
   ```

3. **Execution**
   ```bash
   dotnet run --project BloodLink.API
   ```

4. **API Testing**
   Access the Swagger UI at `https://localhost:[PORT]/swagger` to test the endpoints.

## 🧪 Testing

The solution includes a dedicated test project focused on domain logic and application use cases:
```bash
dotnet test
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

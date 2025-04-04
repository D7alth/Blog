# Blog - Personal Blog API with Clean Architecture

## 📋 Project Overview

This project implements a RESTful API for a personal blog, developed following Clean Architecture principles and SOLID practices. The project serves as a reference implementation for .NET applications requiring a robust and well-structured architecture.

## Architecture and Design Patterns

### Clean Architecture

The project is structured in layers following Clean Architecture principles:

- **Domain Layer**: Contains domain entities, business rules, and repository interfaces
  - Implements Aggregate Root pattern for transaction management
  - Uses Value Objects for immutable concept encapsulation
  - Defines repository contracts through interfaces

- **Application Layer**: Implements application use cases
  - Uses CQRS pattern with MediatR for command and query separation
  - Implements validation through FluentValidation
  - Defines DTOs for data transfer between layers

- **Infrastructure Layer**: Provides concrete implementations of defined interfaces
  - Implements Repository pattern for data access
  - Uses Entity Framework Core for ORM
  - Implements Unit of Work pattern for transaction management
  - Provides dependency injection configurations

- **API Layer**: Presentation layer
  - Implements RESTful endpoints
  - Uses .NET 8 minimal APIs
  - Defines API contracts through DTOs

## Technologies and Tools

- **.NET 8.0**: Main framework
- **Entity Framework Core**: ORM for data access
- **MediatR**: CQRS pattern implementation
- **FluentValidation**: Command and query validation
- **Docker**: Application containerization
- **SQL Server**: Main database
- **NUnit**: Testing framework
- **Moq**: Mocking framework for tests
- **Bogus**: Fake data generation for tests

## Project Structure

```
src/
├── API/                    # Presentation layer
│   └── Endpoints/         # RESTful endpoints
├── Application/           # Use cases and interfaces
│   ├── Articles/         # Article use cases
│   │   ├── Commands/     # Commands (write)
│   │   ├── Queries/      # Queries (read)
│   │   └── Services/     # Application services
│   └── Common/           # Shared components
├── Domain/               # Entities and business rules
│   ├── Articles/        # Article domain
│   │   ├── Entities/    # Entities
│   │   ├── ValueObjects/# Value objects
│   │   └── Repositories/# Repository interfaces
│   └── Shared/          # Shared components
├── Infrastructure/       # Concrete implementations
│   ├── Configuration/   # Configurations
│   ├── Domain/         # Domain implementations
│   ├── Persistence/    # Data access
│   └── UnitOfWork/     # UoW implementation
└── Tests/              # Unit and integration tests
    ├── Application/    # Use case tests
    └── Domain/        # Domain tests
```

## Testing

The project implements comprehensive test coverage:

- **Unit Tests**: Using NUnit and Moq
- **Domain Tests**: Business rule validation
- **Application Tests**: Use case validation
- **Report Generation**: Using Coverlet and ReportGenerator

To run tests:
```bash
dotnet test
```

To generate coverage report:
```bash
./Coverage-report.sh
```

## Running the Project

### Prerequisites

- .NET 8.0 SDK
- SQL Server (or SQLite for development)
- Docker (optional)

### Environment Setup

1. Clone the repository:
```bash
git clone [REPOSITORY_URL]
```

2. Configure connection string in `appsettings.json`

3. Restore dependencies:
```bash
dotnet restore
```

4. Run migrations:
```bash
dotnet ef database update --project src/Infrastructure --startup-project src/API
```

5. Run the project:
```bash
dotnet run --project src/API
```

### Using Docker

1. Build the image:
```bash
docker build -t blog-api .
```

2. Run the container:
```bash
docker run -p 5000:80 blog-api
```

## Code Conventions

- C# naming conventions
- SOLID principles implementation
- API and complex method documentation
- Validation across all layers
- Consistent exception handling

## CI/CD

The project is configured for:
- Automated builds
- Test execution
- Coverage report generation
- Docker containerization

## 📄 License

This project is licensed under the MIT License - see the `LICENSE` file for details.

## 👥 Author

- Alberth - *Initial work*

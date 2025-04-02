# Blog - Personal Blog Project with Clean Architecture

## 🎯 Project Overview

This project is a personal blog API built with Clean Architecture principles and SOLID practices. It serves as the backend foundation for Alberth's personal blog, designed to be robust, scalable, and well-structured. While this is a real project, its architecture and implementation patterns can be used as a template for future projects.

### Key Features

- Clean Architecture implementation with clear separation of concerns
- SOLID principles and best practices
- Effective use of design patterns
- Comprehensive test coverage
- Docker containerization
- Modern development tools and practices

## 🏗️ Architecture

The project follows Clean Architecture principles, organized in layers:

- **Domain**: Core business entities and rules
- **Application**: Use cases and interfaces
- **Infrastructure**: Concrete implementations of application interfaces
- **API**: Presentation layer and controllers
- **Tests**: Unit and integration tests

## 🛠️ Technologies

- **.NET 8.0**: Main framework
- **Docker**: Application containerization
- **Git**: Version control
- **Husky**: Git hooks for code quality
- **VS Code**: Recommended IDE

## 📋 Prerequisites

- .NET 8.0 SDK
- Docker (optional, for containerization)
- Git

## 🚀 Getting Started

### Local Development

1. Clone the repository:
```bash
git clone [REPOSITORY_URL]
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
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

## 🧪 Testing

The project includes unit and integration tests. To run:

```bash
dotnet test
```

To generate coverage report:
```bash
./Coverage-report.sh
```

## 📦 Project Structure

```
src/
├── API/           # Presentation layer
├── Application/   # Use cases and interfaces
├── Domain/        # Business entities and rules
├── Infrastructure/# Concrete implementations
└── Tests/         # Unit and integration tests
```

## 🔧 Development Environment Setup

1. Install recommended VS Code extensions:
   - C#
   - .NET Core Test Explorer
   - Docker

2. Configure Git Hooks:
```bash
git config core.hooksPath .husky
```

## 📝 Code Conventions

- Follow C# naming conventions
- Apply Clean Code principles
- Keep tests up to date
- Document APIs and complex methods

## 🔄 CI/CD

The project is configured for:
- Automated builds
- Test execution
- Coverage report generation
- Docker containerization

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the `LICENSE` file for details.

## 👥 Author

- Alberth - *Initial work*

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- .NET Community
- All contributors 
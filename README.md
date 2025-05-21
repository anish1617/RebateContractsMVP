# Rebate Contracts

This project is a robust, maintainable .NET 9 Web MVC application for managing rebate contracts, built using Clean Architecture. It includes:

- ASP.NET Core MVC for the web layer
- EF Core (Code First) for data access
- xUnit and FluentAssertions for comprehensive unit testing
- Tailwind CSS for modern, responsive UI
- Strict separation of concerns between Domain, Application, Infrastructure, and Web layers

## Getting Started

1. **Restore dependencies:**
   ```pwsh
   dotnet restore
   ```
2. **Build the solution:**
   ```pwsh
   dotnet build
   ```
3. **Run the web app:**
   ```pwsh
   dotnet run --project ./RebateContracts.Web
   ```
4. **Run tests:**
   ```pwsh
   dotnet test
   ```

## Project Structure

- `RebateContracts.Domain` – Core business entities, value objects, domain services
- `RebateContracts.Application` – Use cases, business logic, DTOs, interfaces
- `RebateContracts.Infrastructure` – EF Core DbContext, repositories, data import
- `RebateContracts.Web` – ASP.NET Core MVC, Razor Views, Tailwind CSS
- `RebateContracts.Tests` – xUnit and FluentAssertions tests for all business logic

## Conventions
- All business logic is in Domain or Application layers
- No business logic in controllers or views
- All code is unit tested
- UI is styled only with Tailwind CSS

See `.github/copilot-instructions.md` for detailed architectural and coding guidelines.

<!-- Use this file to provide workspace-specific custom instructions to Copilot. For more details, visit https://code.visualstudio.com/docs/copilot/copilot-customization#_use-a-githubcopilotinstructionsmd-file -->

# GitHub Copilot Instructions: .NET 9 Web MVC Project – Rebate Contracts

## Purpose
Guide Copilot to generate a robust, maintainable .NET 9 Web MVC app called **Rebate Contracts**. Implement all business logic, data relationships, UI, and **comprehensive unit tests**.

---

## 1. Project Architecture
- **Pattern:** Always use Clean Architecture or Layered MVC for a scalable, maintainable codebase.
- **Layer Responsibilities:**
  - **Presentation:** ASP.NET MVC Controllers & Razor Views (no business logic)
  - **Application:** Orchestrate business rules, services, DTOs
  - **Domain:** Entities (use classes), Value Objects, Domain Services (centralize rules & invariants)
  - **Infrastructure:** EF Core DbContext, Repositories, Data Access
- **Organization:**
  - Group by feature (e.g., Contracts, Products, Rebates)
  - Use folders: `/Domain`, `/Application`, `/Infrastructure`, `/Web`
  - Strict separation of concerns between layers

---

## 2. Coding Standards
- **.NET 9 & C# 13:**
  - Use latest language features where appropriate (primary constructors, collection expressions, etc.)
  - File-scoped namespaces, expression-bodied members, nullability annotations
  - Use `class` for domain entities—not `record`—to support EF Core features
  - Use `record` only for immutable value objects or DTOs
  - PascalCase for types/members, camelCase for locals/params
  - Write clean, readable, efficient code
  - Document public APIs & business logic with XML comments

---

## 3. Entity Framework Core (Code First) – Best Practices
- **Modeling:**
  - Always use **class** for domain entities; use **record** for value objects or DTOs only
  - Define navigation properties for all relationships (one-to-many, many-to-many, etc.)
  - Use explicit foreign key properties where possible
  - Mark primary keys with `[Key]` attribute or configure with Fluent API
- **Configuration:**
  - Use Fluent API in `OnModelCreating` for relationships and advanced mapping
  - Use IEntityTypeConfiguration<> for large entity models
- **Migrations (PowerShell):**
  - Add migration: `dotnet ef migrations add MigrationName`
  - Update DB: `dotnet ef database update`
- **Seeding:**
  - Use `HasData` or custom scripts for seeding from CSVs

---

## 4. Business Logic & Testing

### Implementation
- Centralize all business rules in the Domain or Application layer
- Implement all formulas for rebate calculations (percentage-based, per-MT, tiered, conversions, adjustments)
- Enforce validation and domain constraints in entities/services
- Logic must be testable, reusable, and separate from controllers/views

### **Testing Requirements**
- **Write thorough unit tests for every piece of business logic**
- **Test all calculation branches, edge cases, and contract types**
- **Use xUnit and FluentAssertions**
- **Organize tests in a dedicated test project: `[ProjectName].Tests`**
- **Adopt test-driven development (TDD) wherever possible**
- **Include negative and boundary tests**
- **For each new service or logic, also generate a matching test class**

#### Test Project Structure Example:
```
/RebateContracts.Tests
  /Application
    /Services
      - PercentageRebateCalculatorTests.cs
      - TieredRebateCalculatorTests.cs
      - PerMtRebateCalculatorTests.cs
      - ConcentrationConversionServiceTests.cs
  /Domain
    /Entities
      - RebateContractTests.cs
      - BusinessUnitTests.cs
  /Infrastructure
    /Repositories
      - RebateRepositoryTests.cs
```

---

## 5. Frontend / Views
- Use Razor Views for UI; style with Tailwind CSS (via npm)
- Never use inline styles or other CSS frameworks
- Use ViewModels to decouple presentation and domain logic
- Use partial views/components for reusable UI
- Follow responsive and accessible design patterns

---

## 6. Additional Guidelines
- **Dependency Injection:** Register services/repositories/DbContext with .NET DI in `Program.cs`
- **Error Handling:** Implement global error handling; user-friendly error pages
- **Logging:** Use `Microsoft.Extensions.Logging`
- **Configuration:** Use `appsettings.json` and environment variables
- **Async/Await:** Use async/await for all data access and long-running operations

---

## 7. Data Model Reference
- **Key Entities:** RebateContract, BusinessUnit, Product, Supplier, GlobalDemand, RebateRule, ConcentrationConversion, QuantityAdjustment, Sale, etc.

---

## 8. Domain-Specific Copilot Prompts

**Use these templates in Copilot Chat or as code comments to generate code and tests that align with project rules and data:**

### Entity Example  
```
Create a C# class `RebateContract` with properties:
- Id (GUID, primary key)
- Name (string, required)
- ContractType (enum: Percentage, PerMT, Tiered, etc.)
- StartDate (DateTime)
- EndDate (DateTime)
- ICollection<BusinessUnit> EligibleBusinessUnits
- ICollection<RebateRule> RebateRules

Configure all relationships for EF Core Code First using Fluent API. Avoid using records, use classes for domain entities.
```

### Business Logic Example  
```
Implement a service method for "Tiered Rate Payable" rebate as per rebate_rate_payable.csv:
- Input: Total purchased volume, price, list of volume ranges, and rebate rates.
- Logic: For each range, if volume > start, rebate for range = (min(volume, end) - start) × price × rate.
- Sum rebates for all applicable ranges and return total.
- Make the method async and unit-testable.
```

### Unit Test Example  
```
Write an xUnit test for the [ServiceOrEntity] class's [MethodName].
- Arrange: Set up inputs for happy path, edge cases, and invalid scenarios.
- Act: Call the method under test.
- Assert: The returned value matches the expected outcome.
- Add additional tests for boundary and negative scenarios.
```

### Data Import Prompt  
```
Create a PowerShell command and C# service to import data from business CSVs into the database using EF Core.
PowerShell Example:
dotnet run --project ./RebateContracts.Infrastructure/ -- import-csv "datas/CountryMapping.csv"
```

---

## 9. Do’s and Don’ts
**Do:**
- Use classes for entities (not records)
- Use records for value objects or DTOs only
- Use Fluent API for complex EF Core configuration
- Use PowerShell-compatible commands for terminal instructions
- Write clear XML documentation on all public types/methods
- Reference provided data for accurate modeling
- **Write and maintain comprehensive unit tests for all business logic**

**Don’t:**
- Don’t put business logic in controllers or views
- Don’t use inline styles or non-Tailwind CSS
- Don’t use records for entities that require EF Core features
- Don’t mix data access code with business logic
- Don’t hardcode configuration or secrets
- **Don’t skip writing unit tests for any service or logic**

---

## 10. Summary
Build a robust, maintainable, and modern .NET 9 MVC web app for managing rebate contracts, implementing all business rules and relationships from provided data, and following best practices and conventions above. **Always generate and maintain thorough unit tests for all business logic and calculations.**

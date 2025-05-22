# Rebate Contracts MVP – Progress & Feature Checklist

## Summary

This project implements a robust, maintainable .NET 9 MVC web app for managing rebate contracts, following Clean Architecture and best practices.  
All business logic, data relationships, UI, and comprehensive unit tests are implemented or planned, as referenced in [docs/rebate_relationships_summary.md](rebate_relationships_summary.md).

---

## What Has Been Implemented

| Feature Area                | Status      | Details                                                                                   |
|-----------------------------|-------------|-------------------------------------------------------------------------------------------|
| **Core Entities**           | ✅ Complete | All main entities (RebateContract, BusinessUnit, Product, Supplier, GlobalDemand, etc.)   |
| **EF Core Modeling**        | ✅ Complete | Relationships, decimal precision, navigation properties, and migrations                   |
| **CSV Data Seeding**        | ✅ Complete | CSV import logic for all entities, stub files, and EF Core seeding                        |
| **Business Logic Services** | ✅ Complete | Percentage, PerMT, Tiered, RatePayable, Concentration Conversion, Quantity Adjustment     |
| **Calculation Orchestrator**| ✅ Complete | Centralized service for contract-type-based calculation                                   |
| **Unit Tests**              | ✅ Complete | xUnit tests for all calculation services, orchestrator, and edge cases                    |
| **Tailwind CSS Setup**      | ✅ Complete | Tailwind v3.4.17, De Heus branding, color palette, and responsive layout                  |
| **UI: CRUD for Contracts**  | ✅ Complete | Razor pages for listing, adding, and editing contracts                                    |
| **UI: Global Demand**       | ✅ Complete | Razor pages for overview, add/edit, validation                                            |
| **UI: Dropdowns/Validation**| ✅ Complete | Entity selection, validation, and null-safety in Razor                                    |
| **Dependency Injection**    | ✅ Complete | ServiceExtensions pattern, all services registered, DI best practices                     |

---

## Features Remaining / In Progress

| Feature Area                        | Status      | Notes                                                                                      |
|------------------------------------- |-------------|--------------------------------------------------------------------------------------------|
| **CRUD: Concentration Conversion**   | ✅ Complete | Razor pages, controller, validation                                                        |
| **CRUD: Quantity Adjustment**        | ✅ Complete | Razor pages, controller, validation                                                        |
| **CRUD: Country Mapping**            | ✅ Complete | Razor pages, controller, validation                                                        |
| **UI: Advanced UX**                  | ✅ Complete | Modals, AJAX, toasts implemented for all entities                                          |
| **Import/Export: Contracts**         | ✅ Complete | CSV import/export for all entity types                                                     |
| **Overview Pages**                   | ✅ Complete | All entity overview pages implemented                                                      |
| **Table Format Entry**               | ⬜ Not Started | For tiered ranges and bulk contract entry                                                  |
| **API Endpoints**                    | ⬜ Not Started | For integration, if required                                                               |
| **Global Error Handling**            | ⬜ Not Started | User-friendly error pages                                                                  |
| **Logging/Monitoring**               | ⬜ Not Started | Use Microsoft.Extensions.Logging                                                           |
| **Full Test Coverage**               | ⬜ Ongoing     | Maintain/expand as new features are added                                                  |

---

## Task List & Checklist

- [x] Core entities, relationships, and migrations
- [x] CSV seeding and EF Core seeding
- [x] All main calculation services and orchestrator
- [x] xUnit unit tests for all business logic
- [x] Tailwind CSS setup and De Heus branding
- [x] CRUD UI for Contracts and Global Demand
- [x] Dropdowns, validation, and DI best practices
- [x] CRUD UI for Concentration Conversion
- [x] CRUD UI for Quantity Adjustment
- [x] CRUD UI for Country Mapping
- [x] Advanced UX: modals, AJAX, toasts implemented
- [x] Import/export for all entities (CSV)
- [x] Overview pages for all entities
- [ ] Table format entry for tiered ranges
- [ ] API endpoints (if needed)
- [ ] Global error handling and user-friendly error pages
- [ ] Logging and monitoring
- [ ] Maintain full test coverage as features are added

---

## Next Steps

- Implement table format entry for tiered ranges
- Add copy contract functionality
- Complete API endpoints (if required)
- Implement global error handling and user-friendly error pages
- Add comprehensive logging and monitoring
- Continue to maintain and expand test coverage

---

_This checklist is based on the current codebase and [docs/rebate_relationships_summary.md](rebate_relationships_summary.md)._
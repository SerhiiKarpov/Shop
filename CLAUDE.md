# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
# Build the entire solution
dotnet build Shop.slnx

# Run the API
dotnet run --project Shop.Api

# Run tests (no test projects yet)
dotnet test Shop.slnx
```

## Architecture

Four-project .NET 10 solution with a layered architecture:

```
Shop.Api              → ASP.NET Core Minimal API (entry point, DI wiring)
Shop.Contracts        → Domain entities (shared across layers)
Shop.Data             → EF Core DbContext + repository/unit-of-work implementations
Shop.Data.Contracts   → Repository and unit-of-work interfaces
```

**Dependency direction:** `Api → Data → Data.Contracts ← Contracts`

### Key Patterns

**Unit of Work + Repository** — `IUnitOfWork` (in `Shop.Data.Contracts`) owns typed `IRepository<T>` accessors and `SaveChangesAsync`. `UnitOfWork` (in `Shop.Data`) implements this on top of EF Core's `DbContext`. New aggregate roots get a property on `IUnitOfWork` and a corresponding `DbSet` on the `DbContext`.

**DI registration** — `Shop.Data` exposes `IServiceCollection` extension methods (`ServiceCollectionExtensions`). Call these from `Program.cs`; don't register data-layer services directly in `Program.cs`.

**Entity placement** — All domain entities live in `Shop.Contracts/Entities/`. They are plain C# classes; persistence concerns (configuration, migrations) stay in `Shop.Data`.

**Database** — Currently uses EF Core In-Memory provider. To switch, change the provider registration in `Shop.Data/Extensions/ServiceCollectionExtensions.cs`.

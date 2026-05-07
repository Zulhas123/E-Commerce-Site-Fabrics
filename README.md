# Fabrics E‑Commerce — Clean Architecture (ASP.NET Core MVC)

Production‑ready refactor of a classic MVC + EF Core + Identity application into a clean, scalable, maintainable solution following **Clean Architecture** and **SOLID**.

Key goals:
- Clear separation of concerns (Domain / Application / Infrastructure / Presentation)
- Async-first data access + repository pattern
- Dependency injection with interface-based design
- Minimal, professional UI focused on clarity and consistency
- Lightweight dependency set (kept only what’s necessary)
- JWT support for API clients (in addition to cookie auth for MVC)

---

## Architecture overview

This solution follows a layered design:

- **Domain**: Core business entities and rules (no EF, no web, no DI).
- **Application**: Use-cases and service abstractions; depends only on Domain.
- **Infrastructure**: EF Core + Identity + repository implementations; depends on Domain + Application.
- **Presentation (Web)**: MVC UI, controllers, views; composes DI and delegates work to Application services.

### Dependency direction
`Web → Application → Domain`  
`Web → Infrastructure → (Application, Domain)`

---

## Folder structure

```
E-Commerce-Site-Fabrics/
  src/
    ECommerce.Domain/
    ECommerce.Application/
    ECommerce.Infrastructure/
    ECommerce.Web/
  Dockerfile
  docker-compose.yml
  Directory.Build.props
  README.md
```

### Layer details
- `src/ECommerce.Domain`
  - Domain entities (existing entity types kept compatible with the current DB/migrations).
- `src/ECommerce.Application`
  - Repository interfaces: `Abstractions/Persistence/*`
  - Use-case services: `Abstractions/Services/*` + `Services/*`
  - DI entrypoint: `DependencyInjection.cs`
- `src/ECommerce.Infrastructure`
  - EF Core DbContext + migrations: `Persistence/Data/*`
  - Identity user: `Identity/ApplicationUser.cs`
  - Repository implementations: `Persistence/Repositories/*`
  - DI entrypoint: `DependencyInjection.cs`
- `src/ECommerce.Web`
  - MVC + Areas + Views + wwwroot
  - Composition root: `Startup.cs`
  - JWT token endpoint: `Controllers/AuthController.cs`
  - UI layout + partials: `Views/Shared/*`

---

## Features

- Product catalog (list/details) + admin CRUD
- Product type CRUD
- Special tag CRUD
- Orders checkout flow (session-based cart → order + order details)
- Identity (register/login/manage)
- JWT bearer authentication for API clients (`/api/auth/login`)
- Docker Compose setup (SQL Server + web app)

---

## Application flow (high level)

1. **Web controllers** receive requests (MVC routes + Areas).
2. Controllers call **Application services** (interfaces) rather than EF directly.
3. Application services orchestrate rules and call **repository interfaces**.
4. **Infrastructure repositories** execute EF Core queries/commands asynchronously.
5. Views render data via a minimal shared layout and reusable partials.

---

## UI/UX principles applied

- Single clean shared layout using local Bootstrap assets (no third‑party external theme pulls).
- Reusable partials for alerts + row actions.
- Consistent typography, spacing, and table/card patterns.
- Removed brittle/unused UI scripts and CDN dependencies.

---

## Dependencies (essential)

Runtime/framework:
- .NET 8 (projects target `net8.0`)
- ASP.NET Core MVC

Infrastructure:
- EF Core SQL Server
- ASP.NET Core Identity (EF stores + UI)

Presentation:
- Bootstrap (static asset under `wwwroot/lib`)
- jQuery + unobtrusive validation (for built-in MVC/Identity forms)
- X.PagedList (existing customer paging)

Auth:
- JWT bearer (`Microsoft.AspNetCore.Authentication.JwtBearer`)

---

## Configuration

### Connection string
`ConnectionStrings:DefaultConnection` (or environment variable `ConnectionStrings__DefaultConnection`).

### JWT
Configured in `src/ECommerce.Web/appsettings.json` under `Jwt` (or environment variables):
- `Jwt__Key` (required; use a strong secret in real environments)
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__ExpiryMinutes`

Token endpoint:
- `POST /api/auth/login`
- Body:
  ```json
  { "email": "user@example.com", "password": "your-password" }
  ```

---

## Run locally (Windows / PowerShell)

From `E-Commerce-Site-Fabrics/`:

```powershell
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE='1'
$env:DOTNET_CLI_HOME="$PWD\\.dotnet_home"
$env:NUGET_PACKAGES="$PWD\\.nuget\\packages"
$env:MSBuildUserExtensionsPath="$PWD\\.msbuild"

dotnet restore .\\src\\ECommerce.Web\\ECommerce.Web.csproj
dotnet run --project .\\src\\ECommerce.Web\\ECommerce.Web.csproj
```

Then open:
- `https://localhost:5001` / `http://localhost:5000` (Kestrel defaults)  
  (port may vary; check console output)

---

## Docker (SQL Server + Web)

```bash
docker compose up --build
```

Services:
- SQL Server: `localhost:1433`
- Web: `http://localhost:8080`

Environment overrides are configured in `docker-compose.yml`.

---

## Refactor process (what was done, step-by-step)

1. **Analyze current structure**
   - Identified tight coupling: controllers directly used EF DbContext + mixed concerns in views/layout.
2. **Redesign architecture**
   - Introduced Domain/Application/Infrastructure/Web projects and enforced dependency direction.
3. **Progressively refactor each layer**
   - Moved DbContext + migrations to Infrastructure.
   - Added repository interfaces in Application and implementations in Infrastructure.
   - Refactored controllers to depend on Application services (async).
4. **Improve cross-cutting concerns**
   - Centralized DI registration (`AddApplication`, `AddInfrastructure`).
   - Added JWT bearer auth + a minimal login/token API endpoint.
5. **Deliver a professional minimal UI**
   - Replaced bloated layout with a clean, local Bootstrap-based layout.
   - Added shared alerts + simplified reusable partials.
   - Removed unused/brittle UI scripts and external CDNs.
6. **Production readiness**
   - Added Dockerfile + docker-compose for repeatable runs.
   - Added `.gitignore` to avoid committing build outputs and local caches.

---

## Notes / next hardening steps

- Replace the development JWT key with a secure secret (env var / secret manager).
- Add automated tests (unit tests for Application services; integration tests for repositories).
- Consider adding role-based authorization around Admin area routes.
- Address nullable warnings in entities if you want strict NRT compliance.

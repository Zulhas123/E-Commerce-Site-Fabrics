# E-Commerce Site (Fabrics) — Clean Architecture

## Structure
- `src/ECommerce.Domain`: Domain entities
- `src/ECommerce.Application`: Application services + interfaces (use-cases)
- `src/ECommerce.Infrastructure`: EF Core + Identity + repository implementations
- `src/ECommerce.Web`: MVC presentation layer (controllers/views) + JWT endpoint

## Run locally
From `E-Commerce-Site-Fabrics/`:

```powershell
$env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE='1'
$env:DOTNET_CLI_HOME="$PWD\\.dotnet_home"
$env:NUGET_PACKAGES="$PWD\\.nuget\\packages"
$env:MSBuildUserExtensionsPath="$PWD\\.msbuild"
dotnet restore .\\src\\ECommerce.Web\\ECommerce.Web.csproj
dotnet run --project .\\src\\ECommerce.Web\\ECommerce.Web.csproj
```

## JWT
- Configure `Jwt:*` in `src/ECommerce.Web/appsettings.json` (or environment variables like `Jwt__Key`).
- Token endpoint: `POST /api/auth/login` with JSON `{ "email": "...", "password": "..." }`.

## Docker
```bash
docker compose up --build
```

The compose file starts:
- SQL Server on `localhost:1433`
- Web app on `http://localhost:8080`


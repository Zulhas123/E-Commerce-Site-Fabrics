FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./src ./src
COPY ./Directory.Build.props ./

RUN dotnet restore ./src/ECommerce.Web/ECommerce.Web.csproj
RUN dotnet publish ./src/ECommerce.Web/ECommerce.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","ECommerce.Web.dll"]


# Build mərhələsi (.NET 10 SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Bütün solution və csproj fayllarını kopyalayırıq
COPY ["CRUD REST API.slnx", "./"]
COPY ["CRUD REST API/CRUD REST API.csproj", "CRUD REST API/"]
COPY ["CRUD_REST_API.Business/CRUD_REST_API.Business.csproj", "CRUD_REST_API.Business/"]
COPY ["CRUD_REST_API.Core/CRUD_REST_API.Core.csproj", "CRUD_REST_API.Core/"]
COPY ["CRUD_REST_API.DataAccess/CRUD_REST_API.DataAccess.csproj", "CRUD_REST_API.DataAccess/"]
COPY ["CRUD_REST_API.Tests/CRUD_REST_API.Tests.csproj", "CRUD_REST_API.Tests/"]

# Paketləri yükləyirik
RUN dotnet restore "CRUD REST API/CRUD REST API.csproj"

# Qalan bütün kodları kopyalayıb publish edirik
COPY . .
WORKDIR "/src/CRUD REST API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime mərhələsi (Yalnız işləmək üçün lazım olan .NET 10 AspNet Core image)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render-in verdiyi portu avtomatik götürməsi üçün
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CRUD REST API.dll"]
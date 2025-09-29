# ============================
# 🌐 BASE (RUNTIME) STAGE
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Render expects apps to listen on port 8080
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# ============================
# 🛠️ BUILD STAGE
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies
COPY ["StockIT/StockIT.csproj", "StockIT/"]
COPY ["StockIT.BLL/StockIT.BLL.csproj", "StockIT.BLL/"]
RUN dotnet restore "./StockIT/StockIT.csproj"

# Copy the rest of the code and build
COPY . .
WORKDIR "/src/StockIT"
RUN dotnet build "./StockIT.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ============================
# 📦 PUBLISH STAGE
# ============================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StockIT.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ============================
# 🚀 FINAL STAGE
# ============================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockIT.dll"]

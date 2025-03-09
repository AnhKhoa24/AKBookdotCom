# Sử dụng ASP.NET Core runtime làm base image cho production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Sử dụng .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Sao chép file .csproj vào container và thực hiện restore dependencies
COPY ["AKBookdotCom.csproj", "./"]
RUN dotnet restore "./AKBookdotCom.csproj"

# Sao chép toàn bộ source code và build ứng dụng
COPY . ./
WORKDIR "/src/"
RUN dotnet build "./AKBookdotCom.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish ứng dụng sang thư mục /app/publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AKBookdotCom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Tạo container final để chạy ứng dụng
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "AKBookdotCom.dll"]
ENTRYPOINT ["dotnet", "AKBookdotCom.dll", "--urls", "http://*:5135"]


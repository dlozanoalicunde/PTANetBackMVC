FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000
ENV DB_CONNECTION_STRING="Server=172.21.128.1,1433;Database=BanksDB;User Id=sa;Password=admin123;TrustServerCertificate=True;"

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["BankAPI.csproj", "./"]
RUN dotnet restore "BankAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BankAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BankAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankAPI.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AlicundeTest.API/AlicundeTest.API.csproj", "AlicundeTest.API/"]
COPY ["AlicundeTest.Application/AlicundeTest.Application.csproj", "AlicundeTest.Application/"]
COPY ["AlicundeTest.Domain/AlicundeTest.Domain.csproj", "AlicundeTest.Domain/"]
COPY ["AlicundeTest.Persistence/AlicundeTest.Persistence.csproj", "AlicundeTest.Persistence/"]
RUN dotnet restore "./AlicundeTest.API/AlicundeTest.API.csproj"
COPY . .
WORKDIR "/src/AlicundeTest.API"
RUN dotnet build "./AlicundeTest.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AlicundeTest.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlicundeTest.API.dll"]
# Stage 1: Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY Household.Budget.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build --no-restore --configuration Release --output ./build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build-env /app/build .
CMD ["dotnet", "Household.Budget.dll"]
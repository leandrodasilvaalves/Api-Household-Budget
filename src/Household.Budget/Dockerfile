# Stage 1: Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY Household.Budget.csproj .
RUN dotnet restore Household.Budget.csproj

COPY . .
RUN dotnet build Household.Budget.csproj --no-restore --configuration Release --output ./build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build-env /app/build .
CMD ["dotnet", "Household.Budget.dll"]
HEALTHCHECK CMD curl --fail http://localhost:80/hc || exit
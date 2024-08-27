# Use the official .NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files to the container
COPY ["CarStockApi/CarStockApi.csproj", "CarStockApi/"]
RUN dotnet restore "CarStockApi/CarStockApi.csproj"

# Copy the rest of the app files and build the app
COPY . .
WORKDIR "/src/CarStockApi"
RUN dotnet build "CarStockApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarStockApi.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarStockApi.dll"]

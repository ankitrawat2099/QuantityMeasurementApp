# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the entire solution (all projects)
COPY ../ ./

# Restore only the Web API project (includes all dependencies)
RUN dotnet restore QuantityMeasurementWebApi/QuantityMeasurementWebApi.csproj

# Publish the Web API project
RUN dotnet publish QuantityMeasurementWebApi/QuantityMeasurementWebApi.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy the published output
COPY --from=build /app/QuantityMeasurementWebApi/out ./

# Expose port for Render
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Run the API
ENTRYPOINT ["dotnet", "QuantityMeasurementWebApi.dll"]
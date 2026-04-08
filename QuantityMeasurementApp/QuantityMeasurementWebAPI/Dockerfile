# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the entire solution relative to Docker build context
COPY ../ ./

# Restore all dependencies for the Web API project
RUN dotnet restore QuantityMeasurementWebAPI/QuantityMeasurementWebAPI.csproj

# Publish the Web API project
RUN dotnet publish QuantityMeasurementWebAPI/QuantityMeasurementWebAPI.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy published files
COPY --from=build /app/QuantityMeasurementWebAPI/out ./

# Expose port for Render
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Run the API
ENTRYPOINT ["dotnet", "QuantityMeasurementWebAPI.dll"]
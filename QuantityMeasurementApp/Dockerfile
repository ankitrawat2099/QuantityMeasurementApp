# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy entire solution into /src
COPY ./ ./    

# Restore dependencies for WebAPI project
RUN dotnet restore QuantityMeasurementWebAPI/QuantityMeasurementWebAPI.csproj

# Publish the WebAPI project
RUN dotnet publish QuantityMeasurementWebAPI/QuantityMeasurementWebAPI.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish ./

# Expose port for Render
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Run the WebAPI
ENTRYPOINT ["dotnet", "QuantityMeasurementWebAPI.dll"]
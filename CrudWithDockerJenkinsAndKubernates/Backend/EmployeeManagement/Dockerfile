# Use the runtime-only image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Copy published files to the runtime image
COPY ./out ./

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "EmployeeManagement.dll"]

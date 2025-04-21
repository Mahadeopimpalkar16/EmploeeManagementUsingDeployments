using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Fetch the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       "Server=host.docker.internal,1433;Database=EmployeeDB;User Id=Admin;Password=Admin@123;TrustServerCertificate=True;";
Console.WriteLine($"Connection String: {connectionString}"); // Debugging purpose

// Add services to the container
builder.Services.AddControllers();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        // Add retry logic for transient errors
        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null);
    }));

// Dependency Injection for Repository Layer
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
// Configure CORS to allow requests from any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

// Ensure the application listens on all network interfaces
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// Build the app
var app = builder.Build();

// Enable Swagger UI regardless of environment
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

// Apply CORS Policy
app.UseCors("AllowAll");

// Use routing and endpoints for controllers
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Run the application to keep it alive
app.Run();

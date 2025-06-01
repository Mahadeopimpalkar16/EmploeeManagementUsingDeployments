using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddSwaggerGen( options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(new
                {
                    message = "Invalid token."
                }.ToString());
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(new
                {
                    message = "Token is required."
                }.ToString());
            }
        };
    });
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Run the application to keep it alive
app.Run();

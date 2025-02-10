using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();

// (Optional) If you have a user service/repository, register it here.
// For demonstration, we might register an in-memory repository as a singleton.
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

var app = builder.Build();

// Global exception handling (only expose minimal info in production)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// Register custom logging middleware so every request is logged
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRouting();

// (Optional) Add authentication/authorization middleware if needed
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();

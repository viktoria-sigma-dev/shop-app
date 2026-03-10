using UsersApp.Middlewares;
using UsersApp.src.Application.Services;
using UsersApp.src.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using UsersApp.src.WebApi.DI;

var builder = WebApplication.CreateBuilder(args);

// === Dependency Injection registrations ===

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// Register DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Dependency Injection
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseRouting();
app.MapControllers();

app.Run();

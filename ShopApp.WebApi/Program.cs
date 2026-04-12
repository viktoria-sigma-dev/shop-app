using ShopApp.Application.Services;
using ShopApp.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using ShopApp.WebApi.DI;
using ShopApp.Application.Mappings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// === Mapping ===
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(UserMapping).Assembly);
});

// === Controllers & Filters ===

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Register DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// === Dependency Injection ===
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// === Swagger / OpenAPI ===
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

app.UseRouting();
app.MapControllers();

app.Run();

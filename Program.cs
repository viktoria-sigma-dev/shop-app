using UsersApp.Repository;
using UsersApp.Services;
using UsersApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// === Dependency Injection registrations ===

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddSingleton<FileService>();

// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CommentRepository>();

// Controllers
builder.Services.AddControllers();

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
app.Services.GetRequiredService<FileService>();

app.MapControllers();

app.Run();

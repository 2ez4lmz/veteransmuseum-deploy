using Serilog;
using VeteransMuseum.Application;
using VeteransMuseum.Infrastructure;
using VeteransMuseum.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Укажите адрес вашего фронтенда
            .AllowAnyMethod() // Разрешаем все HTTP-методы (GET, POST и т.д.)
            .AllowAnyHeader(); // Разрешаем любые заголовки
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();

    app.SeedData();
}

app.UseHttpsRedirection();

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseCustomExceptionHandler();

app.UseRouting();

app.UseAuthentication();
 
app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using ArchPilot.Persistence.Data;
using ArchPilot.Application.Mappings;
using ArchPilot.Application.Services;
using ArchPilot.Application.Interfaces;
using ArchPilot.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var apiBaseUrlHttp = builder.Configuration["ApiSettings:BaseUrlHttp"];
var apiBaseUrlHttps = builder.Configuration["ApiSettings:BaseUrlHttps"];

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins(apiBaseUrlHttp??"",apiBaseUrlHttps??"")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add Entity Framework
builder.Services.AddDbContext<ArchPilotDbContext>(options =>
    options.UseInMemoryDatabase("ArchPilotDb")); // Using in-memory database for demo

// Register the DbContext interface
builder.Services.AddScoped<IArchPilotDbContext>(provider => provider.GetRequiredService<ArchPilotDbContext>());

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ArchPilot.Application.Mappings.MappingProfile).Assembly));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add custom services
builder.Services.AddScoped<IRecommendationEngine, RecommendationEngine>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorWasm");

app.MapControllers();

app.Run();

using Meetly.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Databas – connection string "DefaultConnection":
//  - lokalt: appsettings.Development.json (LocalDB)
//  - Azure: App Service-inställning ConnectionStrings__DefaultConnection (ALDRIG i repot)
builder.Services.AddDbContext<MeetlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS – vilka frontend-URL:er som får anropa API:et läses från konfiguration (Cors:AllowedOrigins)
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Ingen HTTPS-redirect: Azure App Service sköter HTTPS framför containern
app.UseCors("Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

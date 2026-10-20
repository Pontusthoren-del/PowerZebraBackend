using Meetly.Api.Data;
using Meetly.Api.Models;
using Microsoft.AspNetCore.DataProtection;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Lokalt från appsettings.Development.json, i Azure från App Service-inställningarna
builder.Services.AddDbContext<MeetlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sql=>sql.EnableRetryOnFailure()));

// Spara nycklarna i databasen i stället för i containern, annars loggas alla ut vid omstart
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<MeetlyDbContext>();

// Inloggning med ASP.NET Core Identity (färdiga endpoints under /api/auth)
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>(options =>
    {
        // Samma e-post får inte registreras två gånger
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<MeetlyDbContext>();

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

// Kör migreringar vid start, bara om det finns en databas att prata med
if (!string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DefaultConnection")))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<MeetlyDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Ingen HTTPS-redirect: Azure App Service sköter HTTPS framför containern
app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api/auth").MapIdentityApi<User>();

app.Run();

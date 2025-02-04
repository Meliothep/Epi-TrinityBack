using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Trinity.Gateway.DelegatingHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureIdentity();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient();

// 🔹 Register CORS services before using it
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddOcelot();

var app = builder.Build();

app.UseRouting();

// 🔹 Apply the registered CORS policy
app.UseCors("AllowAll");

// **Authentication and Authorization must be before Ocelot**
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureIdentity();
// Add Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001"; // URL of Duende IdentityServer
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false // Skip audience validation if not needed
        };
    });

// Add Ocelot
builder.Services.AddOcelot();

var app = builder.Build();

app.UseAuthentication(); // Add Authentication Middleware
app.UseAuthorization();  // Add Authorization Middleware

await app.UseOcelot(); // Add Ocelot Middleware
app.Run();

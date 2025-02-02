using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Trinity.Gateway.DelegatingHandlers;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureIdentity();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient();

builder.Services.AddTransient<TokenExchangeDelegatingHandler>();

builder.Services.AddOcelot()
    .AddDelegatingHandler<TokenExchangeDelegatingHandler>();

var app = builder.Build();

await app.UseOcelot();

app.Run();

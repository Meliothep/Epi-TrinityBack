using Inventory.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Trinity.EntityModels.DataAccess;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApiDefaults();

builder.AddNpgsqlDbContext<InventoryDbContext>("DefaultConnection");

builder.Services.AddHostedService<ConsumerService>(); 

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class InventoryProgram { }


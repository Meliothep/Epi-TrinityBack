using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

using Inventory.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<InventoryDbContext>("DefaultConnection");

var app = builder.Build();

DatabaseInitializer.CreateDbIfNotExists(app);

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class InventoryProgram { }


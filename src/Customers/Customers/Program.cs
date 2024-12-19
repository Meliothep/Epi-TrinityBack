using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

using Customers.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<CustomersDbContext>("DefaultConnection");

var app = builder.Build();

DatabaseInitializer.CreateDbIfNotExists(app);

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class CustomersProgram { }


using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

 builder.ConfigureSerilog();
 builder.ConfigureScalar();
 builder.AddDefaultHealthChecks();

var app = builder.Build();


 app.AddScalar();

 app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class CustomersProgram { }
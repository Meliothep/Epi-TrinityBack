using Trinity.EntityModels.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.AddObservability();

builder.AddRabbitMQ();

builder.AddApiDefaults();

builder.AddNpgsqlDbContext<InventoryDbContext>("DefaultConnection");

builder.AddRabbitMQ();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class InventoryProgram { }


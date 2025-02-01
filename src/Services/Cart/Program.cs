using Trinity.EntityModels.DataAccess;


var builder = WebApplication.CreateBuilder(args);

builder.AddObservability();

builder.AddApiDefaults();

builder.AddNpgsqlDbContext<CartDbContext>("DefaultConnection");

builder.AddRabbitMQ();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class CartProgram { }


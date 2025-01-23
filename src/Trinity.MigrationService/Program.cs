using Trinity.EntityModels.DataAccess;
using Trinity.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker<CustomerDbContext>>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Trinity.MigrationService"));

builder.AddNpgsqlDbContext<CustomerDbContext>("CustomersDB");
//builder.AddNpgsqlDbContext<InventoryDbContext>("DefaultConnection");

var host = builder.Build();
host.Run();
using Trinity.EntityModels.DataAccess;
using Trinity.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource("Trinity.MigrationService"));

builder.AddNpgsqlDbContext<CustomerDbContext>("CustomersDB");
builder.AddNpgsqlDbContext<InventoryDbContext>("InventoryDB");
builder.AddNpgsqlDbContext<CartDbContext>("CartDB");

builder.Services.AddHostedService<Worker<CustomerDbContext>>();
builder.Services.AddHostedService<Worker<InventoryDbContext>>();    
builder.Services.AddHostedService<Worker<CartDbContext>>();

var host = builder.Build();

host.Run();
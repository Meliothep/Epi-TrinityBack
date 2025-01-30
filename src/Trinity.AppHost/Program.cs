
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var dbUsername = builder.AddParameter("DbUsername", secret: true);
var dbPassword = builder.AddParameter("DbPassword", secret: true);

var broker = builder.AddRabbitMQ("broker", port : 5672 ).WithManagementPlugin();

var postgres = builder.AddPostgres("customerPostgres", dbUsername, dbPassword, 5432);

if(builder.Environment.IsDevelopment()){
        postgres.WithPgAdmin();
}


var postgresdb = postgres.AddDatabase("customersDB");

var migrationService = builder.AddProject<Projects.Trinity_MigrationService>("migrations")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.Customers>("customer")
        .WithReference(broker)
        .WaitFor(broker)
        .WithReference(postgresdb)
        .WaitFor(postgresdb)
        .WaitForCompletion(migrationService);


postgres = builder.AddPostgres("inventoryPostgres", dbUsername, dbPassword, 5433);
postgresdb = postgres.AddDatabase("inventoryDB");

migrationService.WithReference(postgresdb).WaitFor(postgresdb);

builder.AddProject<Projects.Inventory>("inventory")
        .WithReference(broker)
        .WaitFor(broker)
        .WithReference(postgresdb)
        .WaitFor(postgresdb)
        .WaitForCompletion(migrationService);


postgres = builder.AddPostgres("cartPostgres", dbUsername, dbPassword, 5434);
postgresdb = postgres.AddDatabase("cartDB");

migrationService.WithReference(postgresdb).WaitFor(postgresdb);

builder.AddProject<Projects.Cart>("cart")
        .WithReference(broker)
        .WaitFor(broker)
        .WithReference(postgresdb)
        .WaitFor(postgresdb)
        .WaitForCompletion(migrationService);


builder.Build().Run();

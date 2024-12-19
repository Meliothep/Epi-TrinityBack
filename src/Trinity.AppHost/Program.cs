
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var dbUsername = builder.AddParameter("DbUsername", secret: true);
var dbPassword = builder.AddParameter("DbPassword", secret: true);

var kafka = builder.AddKafka("kafka",9092);

var postgres = builder.AddPostgres("customerPostgres", dbUsername, dbPassword, 5432);
if(builder.Environment.IsDevelopment()){
        postgres.WithPgAdmin();
}

var postgresdb = postgres.AddDatabase("customersDB");

builder.AddProject<Projects.Customers>("customer")
        .WithReference(kafka)
        .WaitFor(kafka)
        .WithReference(postgresdb)
        .WaitFor(postgresdb);


postgres = builder.AddPostgres("InventoryPostgres", dbUsername, dbPassword, 5433);
postgresdb = postgres.AddDatabase("inventoryDB");

builder.AddProject<Projects.Inventory>("inventory")
        .WithReference(kafka)
        .WaitFor(kafka)
        .WithReference(postgresdb)
        .WaitFor(postgresdb);;

builder.Build().Run();

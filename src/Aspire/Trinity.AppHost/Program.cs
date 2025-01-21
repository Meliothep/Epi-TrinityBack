
var builder = DistributedApplication.CreateBuilder(args);


var username = builder.AddParameter("DbUsername", secret: true);
var password = builder.AddParameter("DbPassword", secret: true);

var kafka = builder.AddKafka("kafka",9092);

var Cpostgres = builder.AddPostgres("customerPostgres", username, password, 5432).WithPgAdmin();
var Cpostgresdb = Cpostgres.AddDatabase("CustomersDB");

builder.AddProject<Projects.Customers>("customer")
        .WithReference(kafka)
        .WaitFor(kafka)
        .WithReference(Cpostgresdb)
        .WaitFor(Cpostgresdb);

/**
var Ipostgres = builder.AddPostgres("InventoryPostgres", username, password, 5433);
var Ipostgresdb = Ipostgres.AddDatabase("InventoryDB");

builder.AddProject<Projects.Inventory>("inventory")
        .WithReference(kafka)
        .WithReference(Ipostgresdb);
**/
builder.Build().Run();

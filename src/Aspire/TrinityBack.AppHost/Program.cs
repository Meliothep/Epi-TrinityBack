
var builder = DistributedApplication.CreateBuilder(args);


var username = builder.AddParameter("DbUsername", secret: true);
var password = builder.AddParameter("Dbpassword", secret: true);

var postgres = builder.AddPostgres("customerPostgres", username, password, 5432);
var postgresdb = postgres.AddDatabase("CustomersDB");


builder.AddProject<Projects.Customers>("customer")
        .WithExternalHttpEndpoints()
        .WithReference(postgresdb);

builder.Build().Run();

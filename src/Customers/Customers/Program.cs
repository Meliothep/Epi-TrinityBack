using Scalar.AspNetCore;

using Confluent.Kafka;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

using Serilog;
using Serilog.Sinks.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.OpenTelemetry( x => {
                x.Endpoint = "http://localhost:5341/ingest/otlp/v1/logs";
                x.Protocol = OtlpProtocol.HttpProtobuf;
                x.Headers = new Dictionary<string, string>{
                    ["X-Seq-ApiKey"] = builder.Configuration["Seq:X-Seq-ApiKey"]
                };
                x.ResourceAttributes = new Dictionary<string, object>{
                    ["service.name"] = "Customers"
                };
            })
            .CreateLogger();


builder.Services.AddSerilog();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var kafkaUrl = builder.Configuration["Kafka:BootstrapServers"];
var producerConfig = new ProducerConfig { BootstrapServers = kafkaUrl };
using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

var npgSqlString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddHealthChecks()
    .AddKafka(producerConfig, "logs")
    .AddNpgSql(npgSqlString!)
    ;

var app = builder.Build();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class CustomersProgram { }
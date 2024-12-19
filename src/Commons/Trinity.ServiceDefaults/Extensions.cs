using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Sinks.OpenTelemetry;

using Scalar.AspNetCore;

using Confluent.Kafka;
using HealthChecks.UI.Client;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.ConfigureScalar();

        builder.ConfigureSerilog();

        builder.AddDefaultHealthChecks();

        return builder;
    }

    public static IHostApplicationBuilder ConfigureScalar(this IHostApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static IHostApplicationBuilder ConfigureSerilog(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.OpenTelemetry( x => {
                        x.Endpoint = ""; // TODO: GET seq otlp enpoint on appsettings (doc de aspire)
                        x.Protocol = OtlpProtocol.HttpProtobuf;
                        x.Headers = new Dictionary<string, string>{
                            ["X-Seq-ApiKey"] = builder.Configuration["Seq:X-Seq-ApiKey"]
                        };
                        x.ResourceAttributes = new Dictionary<string, object>{
                            ["service.name"] = "" // TODO: GET the name of the service in Appsetting
                        };
                    })
                    .CreateLogger();

        builder.Services.AddSerilog();

        return builder;
    }

    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        
        var kafkaUrl = builder.Configuration["Kafka:BootstrapServers"];
        var producerConfig = new ProducerConfig { BootstrapServers = kafkaUrl };
        using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();

        var npgSqlString = builder.Configuration["ConnectionStrings:DefaultConnection"];

        builder.Services.AddHealthChecks()
            .AddKafka(producerConfig, "logs")
            .AddNpgSql(npgSqlString!);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        app.MapHealthChecks(
            "/health",
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        
        app.AddScalar();

        return app;
    }
    
    public static WebApplication AddScalar(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        return app;
    }
}

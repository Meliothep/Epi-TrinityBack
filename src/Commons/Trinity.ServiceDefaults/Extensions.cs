using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

using Serilog;

using Scalar.AspNetCore;

using Confluent.Kafka;
using HealthChecks.UI.Client;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.ConfigureOpenTelemetry();

        builder.ConfigureScalar();

        builder.AddSeqEndpoint("seq", static settings => 
            {
                settings.DisableHealthChecks  = true;
                settings.ServerUrl = "http://localhost:5341";
            });

        builder.ConfigureSerilog();

        builder.AddDefaultHealthChecks();

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

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation();
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        return builder;
    }

    public static IHostApplicationBuilder ConfigureSerilog(this IHostApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
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
            .AddKafka(producerConfig, "default")
            .AddNpgSql(npgSqlString!);

        return builder;
    }

    public static IHostApplicationBuilder ConfigureScalar(this IHostApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
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

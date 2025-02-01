using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

using Serilog;

using Scalar.AspNetCore;

using HealthChecks.UI.Client;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry;
using MassTransit;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IHostApplicationBuilder AddObservability(this IHostApplicationBuilder builder)
    {
        builder.ConfigureOpenTelemetry();

        builder.ConfigureSerilog();

        return builder;
    }

    public static IHostApplicationBuilder AddRabbitMQ(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x=>{

            x.UsingRabbitMq((ctx,cfg)=>{

            cfg.Host(builder.Configuration["RabbitMQ:Host"],"/" , c=>
            {
                c.Username(builder.Configuration["RabbitMQ:Username"]);
                c.Password(builder.Configuration["RabbitMQ:Password"]);
            });
            
            cfg.ConfigureEndpoints(ctx);
            });
        });

        return builder;
    }

    public static IHostApplicationBuilder AddApiDefaults(this IHostApplicationBuilder builder)
    {
        
        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

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
        var npgSqlString = builder.Configuration["ConnectionStrings:DefaultConnection"];

        builder.Services.AddHealthChecks()
            .AddNpgSql(npgSqlString!);

        return builder;
    }

    public static WebApplication AddScalar(this WebApplication app)
    {
        app.MapOpenApi();
        string? scalarURL = Environment.GetEnvironmentVariable("SCALAR_URLS");
        scalarURL = scalarURL != null ? scalarURL : app.Configuration["Urls"];
        app.MapScalarApiReference(options =>{ options.Servers = [new ScalarServer(scalarURL!)];});
     
        return app;
    }
}
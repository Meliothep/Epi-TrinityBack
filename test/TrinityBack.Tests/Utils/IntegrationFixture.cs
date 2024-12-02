
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.Kafka;
using Testcontainers.PostgreSql;

namespace TrinityBack.Tests.Utils;
public abstract class IntegrationFixture<TEntryPoint> : IAsyncLifetime where TEntryPoint : class
{

    public virtual string ProjectName { get { return "Project"; } }
    
    public virtual int ProjectDBPort { get { return 5432; } }
    
    private readonly PostgreSqlContainer _dbContainer; 

    private readonly KafkaContainer _kafkaContainer = new KafkaBuilder()
            .WithImage("confluentinc/cp-kafka:latest")
            .WithPortBinding(9092)
            .Build();

    public IntegrationFixture(){
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase(ProjectName + "DB")
            .WithUsername(ProjectName)
            .WithPassword(ProjectName + "Secured")
            .WithPortBinding(ProjectDBPort, 5432)
            .Build();
    }

    public WebApplicationFactory<TEntryPoint>? factory;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _kafkaContainer.StartAsync();
        factory = new WebApplicationFactory<TEntryPoint>();
    }

    public async Task DisposeAsync()
    {
        await factory!.DisposeAsync();
        await _dbContainer.DisposeAsync();
        await _kafkaContainer.DisposeAsync();
    }
}
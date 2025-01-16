using Customers.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class DatabaseInitializer{
    public static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TrinityDbContext>();
                Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }

    private static void Initialize(TrinityDbContext context)
    {
        context.Database.EnsureCreated();

        context.SaveChanges();
    }

}
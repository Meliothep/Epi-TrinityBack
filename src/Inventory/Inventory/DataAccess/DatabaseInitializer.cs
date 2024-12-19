using Inventory.DataAccess;

public static class DatabaseInitializer{
    public static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<InventoryDbContext>();
                Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }

    private static void Initialize(InventoryDbContext context)
    {
        context.Database.EnsureCreated();

        context.SaveChanges();
    }

}
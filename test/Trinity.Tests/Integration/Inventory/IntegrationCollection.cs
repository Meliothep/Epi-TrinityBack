namespace Trinity.Tests.Integration.Inventory;

[CollectionDefinition("Inventory Integration collection", DisableParallelization = true)]
public class InventoryIntegrationCollection : ICollectionFixture<InventoryIntegrationFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
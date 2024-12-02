namespace TrinityBack.Tests.Integration.Customers;

[CollectionDefinition("Customers Integration collection", DisableParallelization = true)]
public class CustomersIntegrationCollection : ICollectionFixture<CustomersIntegrationFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
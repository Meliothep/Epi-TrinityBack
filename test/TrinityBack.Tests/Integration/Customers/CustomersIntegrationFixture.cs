using TrinityBack.Tests.Utils;

namespace TrinityBack.Tests.Integration.Customers;


public class CustomersIntegrationFixture : IntegrationFixture<CustomersProgram>
{
    public override string ProjectName => "Customers";
    public override int ProjectDBPort => 5432;
}
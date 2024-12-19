using Trinity.Tests.Utils;

namespace Trinity.Tests.Integration.Customers;


public class CustomersIntegrationFixture : IntegrationFixture<CustomersProgram>
{
    public override string ProjectName => "Customers";
    public override int ProjectDBPort => 5432;
}
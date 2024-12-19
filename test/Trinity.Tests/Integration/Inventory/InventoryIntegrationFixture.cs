using Trinity.Tests.Utils;

namespace Trinity.Tests.Integration.Inventory;


public class InventoryIntegrationFixture : IntegrationFixture<InventoryProgram>
{
    public override string ProjectName => "Inventory";
    public override int ProjectDBPort => 5433;
}
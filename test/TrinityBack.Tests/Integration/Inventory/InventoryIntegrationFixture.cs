using TrinityBack.Tests.Utils;

namespace TrinityBack.Tests.Integration.Inventory;


public class InventoryIntegrationFixture : IntegrationFixture<InventoryProgram>
{
    public override string ProjectName => "Inventory";
    public override int ProjectDBPort => 5433;
}
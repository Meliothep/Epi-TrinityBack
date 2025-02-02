using Trinity.EntityModels.Models;

public class CreateBrandRequest
{
    public required string Name { get; set; }
}

public class UpdateBrandRequest
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }
}

public class BrandResponse
{
    public static BrandResponse MakeBrandResponse(Brand b){
        return new BrandResponse(){
            Id = b.Id,
            Name = b.Name
        };
    }

    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

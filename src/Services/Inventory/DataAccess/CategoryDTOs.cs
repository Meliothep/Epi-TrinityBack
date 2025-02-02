using Trinity.EntityModels.Models;

public class CreateCategoryRequest
{
    public required string Name { get; set; }

    public string? Description { get; set; }
}

public class UpdateCategoryRequest
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }
}

public class CategoryResponse
{
    public static CategoryResponse MakeCategoryResponse(Category c){
        return new CategoryResponse(){ 
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        };
    }

    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}

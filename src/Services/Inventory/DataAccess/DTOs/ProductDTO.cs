using Trinity.EntityModels.Models;

namespace Inventory.DataAccess.DTOs;

public class ProductDTO
{
    public Guid? Id { get; set; }
    public string? IdSupplier { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<BrandDTO> Brands { get; set; } = new List<BrandDTO>();
    public Guid MainCategoryId { get; set; } 
    public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Weight { get; set; }
    public string? ImageUrl { get; set; }

    public List<OriginDTO> Origins { get; set; } = new List<OriginDTO>();

    public List<LabelDTO> Labels { get; set; } = new List<LabelDTO>();

    public List<AllergenDTO> Allergens { get; set; } = new List<AllergenDTO>();

    public string? NutritionalInfo { get; set; }

    public char? NutritionGrade { get; set; }

}

using Microsoft.CodeAnalysis;
using Trinity.EntityModels.Models;

public class ProductDTO
{
    public Guid? Id { get; set; }
    public string IdSupplier { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<BrandDTO> Brands { get; set; } = new List<BrandDTO>();
    public Guid MainCategoryId { get; set; } 
    public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Weight { get; set; }
    public string? ImageUrl { get; set; }

    public List<OriginDTO> Origins { get; set; } = new List<OriginDTO>();

    public List<LabelDTO> Labels { get; set; } = new List<LabelDTO>();

    public List<AllergenDTO> Allergens { get; set; } = new List<AllergenDTO>();

    public string? NutritionalInfo { get; set; }

    public char? NutritionGrade { get; set; }

    public static ProductDTO MakeDTO(Product p)
    {
        return new ProductDTO()
        {
            Id = p.Id,
            IdSupplier = p.IdSupplier,
            Name = p.Name,
            Description = p.Description,
            Brands = p.Brands.ConvertAll(
                    new Converter<Brand, BrandDTO>(BrandDTO.MakeDTO)),
            MainCategoryId = p.MainCategoryId,
            Categories = p.Categories.ConvertAll(
                    new Converter<Category, CategoryDTO>(CategoryDTO.MakeDTO)),
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            Weight = p.Weight,
            ImageUrl = p.ImageUrl,
            Origins = p.Origins.ConvertAll(
                    new Converter<Origin, OriginDTO>(OriginDTO.MakeDTO)),
            Labels = p.Labels.ConvertAll(
                    new Converter<Label, LabelDTO>(LabelDTO.MakeDTO)),
            Allergens = p.Allergens.ConvertAll(
                    new Converter<Allergen, AllergenDTO>(AllergenDTO.MakeDTO)),
            NutritionalInfo = p.NutritionInfo,
            NutritionGrade = p.NutritionGrade
        };
    }

     public static Product MakeModel(ProductDTO dto)
    {
        return new Product()
        {
            Id = dto.Id ?? Guid.Empty,
            IdSupplier = dto.IdSupplier,
            Name = dto.Name,
            Description = dto.Description,
            Brands = dto.Brands.ConvertAll(
                new Converter<BrandDTO, Brand>(BrandDTO.MakeModel)),
            MainCategoryId = dto.MainCategoryId,
            Categories = dto.Categories.ConvertAll(
                new Converter<CategoryDTO, Category>(CategoryDTO.MakeModel)),
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            Weight = dto.Weight,
            ImageUrl = dto.ImageUrl,
            Origins = dto.Origins.ConvertAll(
                new Converter<OriginDTO, Origin>(OriginDTO.MakeModel)),
            Labels = dto.Labels.ConvertAll(
                new Converter<LabelDTO, Label>(LabelDTO.MakeModel)),
            Allergens = dto.Allergens.ConvertAll(
                new Converter<AllergenDTO, Allergen>(AllergenDTO.MakeModel)),
            NutritionInfo = dto.NutritionalInfo,
            NutritionGrade = dto.NutritionGrade
        };
    }
}

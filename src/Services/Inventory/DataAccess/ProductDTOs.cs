using Trinity.EntityModels.Models;

public class CreateProductRequest
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required Guid BrandId { get; set; }

    public required Guid CategoryId { get; set; }

    public string? Barcode { get; set; }

    public required decimal Price { get; set; }

    public required int StockQuantity { get; set; } = 0;

    public string? ImageUrl { get; set; }

    public string? OffId { get; set; }

    public string? NutritionalInfo { get; set; }

    public Product MakeProduct(Brand b, Category c){
        return new Product(){
            Name = Name,
            Description = Description,
            Brand = b,
            Category = c,
            Price = Price,
            Barcode = Barcode,
            StockQuantity = StockQuantity,
            ImageUrl = ImageUrl,
            OffId = OffId,
            NutritionalInfo = NutritionalInfo,
        };
    }
}

public class UpdateProductRequest
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required Guid BrandId { get; set; }

    public required Guid CategoryId { get; set; }

    public string? Barcode { get; set; }

    public required decimal Price { get; set; }

    public required int StockQuantity { get; set; } = 0;

    public string? ImageUrl { get; set; }

    public string? OffId { get; set; }

    public string? NutritionalInfo { get; set; }

    public Product MakeProduct(Brand b, Category c){
        return new Product(){
            Id = Id,
            Name = Name,
            Description = Description,
            Brand = b,
            Category = c,
            Price = Price,
            Barcode = Barcode,
            StockQuantity = StockQuantity,
            ImageUrl = ImageUrl,
            OffId = OffId,
            NutritionalInfo = NutritionalInfo,
        };
    }
}

public class ProductResponse
{
    public static ProductResponse MakeProductResponse(Product p){
        return new ProductResponse(){
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Brand = BrandResponse.MakeBrandResponse(p.Brand),
            Category = CategoryResponse.MakeCategoryResponse(p.Category),
            Price = p.Price,
            Barcode = p.Barcode,
            StockQuantity = p.StockQuantity,
            ImageUrl = p.ImageUrl,
            OffId = p.OffId,
            NutritionalInfo = p.NutritionalInfo,
        };
    }

    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required BrandResponse Brand { get; set; }
    public required CategoryResponse Category { get; set; }
    public string? Barcode { get; set; }
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }
    public string? ImageUrl { get; set; }
    public string? OffId { get; set; }
    public string? NutritionalInfo { get; set; }
}


using Inventory.DataAccess.DTOs;
using Trinity.EntityModels.Models;

namespace Inventory.DataAccess;

public static class DtoToDomainMapper
{
    public static Allergen ToAllergen(this AllergenDTO allergenDTO){
        return new Allergen()
        {
            Id = allergenDTO.Id,
            Name = allergenDTO.Name,
        };
    }
    public static Brand ToBrand(this BrandDTO brandDTO){
        return new Brand()
        {
            Id = brandDTO.Id,
            Name = brandDTO.Name,
        };
    }

    public static Category ToCategory(this CategoryDTO categoryDTO)
    {
        return new Category()
        {
            Id = categoryDTO.Id,
            Name = categoryDTO.Name,
            Description = categoryDTO.Description
        };
    }

    public static Label ToLabel(this LabelDTO labelDTO){
        return new Label()
        {
            Id = labelDTO.Id,
            Name = labelDTO.Name,
        };
    }

    public static Origin ToOrigin(this OriginDTO originDTO){
        return new Origin()
        {
            Id = originDTO.Id,
            Name = originDTO.Name,
        };
    }

    public static Product ToProduct(this ProductDTO productDTO){
        return new Product()
        {
            Id = productDTO.Id ?? Guid.Empty,
            IdSupplier = productDTO.IdSupplier,
            Name = productDTO.Name,
            Description = productDTO.Description,
            Brands = productDTO.Brands.ConvertAll(
                new Converter<BrandDTO, Brand>(x=>x.ToBrand())),
            MainCategoryId = productDTO.MainCategoryId,
            Categories = productDTO.Categories.ConvertAll(
                new Converter<CategoryDTO, Category>(x=>x.ToCategory())),
            Price = productDTO.Price,
            StockQuantity = productDTO.StockQuantity,
            Weight = productDTO.Weight,
            ImageUrl = productDTO.ImageUrl,
            Origins = productDTO.Origins.ConvertAll(
                new Converter<OriginDTO, Origin>(x=>x.ToOrigin())),
            Labels = productDTO.Labels.ConvertAll(
                new Converter<LabelDTO, Label>(x=>x.ToLabel())),
            Allergens = productDTO.Allergens.ConvertAll(
                new Converter<AllergenDTO, Allergen>(x=>x.ToAllergen())),
            NutritionInfo = productDTO.NutritionalInfo,
            NutritionGrade = productDTO.NutritionGrade
        };
    }

}
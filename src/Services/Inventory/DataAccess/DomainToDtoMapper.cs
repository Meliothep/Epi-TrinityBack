
using Inventory.DataAccess.DTOs;
using Trinity.EntityModels.Models;

namespace Inventory.DataAccess;

public static class DomainToDtoMapper
{
    public static AllergenDTO ToAllergenDTO(this Allergen allergen){
        return new AllergenDTO()
        {
            Id = allergen.Id,
            Name = allergen.Name,
        };
    }
    public static BrandDTO ToBrandDTO(this Brand brand){
        return new BrandDTO()
        {
            Id = brand.Id,
            Name = brand.Name,
        };
    }

    public static CategoryDTO ToCategoryDTO(this Category category)
    {
        return new CategoryDTO()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
    }

    public static LabelDTO ToLabelDTO(this Label label){
        return new LabelDTO()
        {
            Id = label.Id,
            Name = label.Name,
        };
    }

    public static OriginDTO ToOriginDTO(this Origin origin){
        return new OriginDTO()
        {
            Id = origin.Id,
            Name = origin.Name,
        };
    }

    public static ProductDTO ToProductDTO(this Product product){
        return new ProductDTO()
        {
            Id = product.Id,
            IdSupplier = product.IdSupplier,
            Name = product.Name,
            Description = product.Description,
            Brands = product.Brands.ConvertAll(
                    new Converter<Brand, BrandDTO>(x=>x.ToBrandDTO())),
            MainCategoryId = product.MainCategoryId,
            Categories = product.Categories.ConvertAll(
                    new Converter<Category, CategoryDTO>(x=>x.ToCategoryDTO())),
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Weight = product.Weight,
            ImageUrl = product.ImageUrl,
            Origins = product.Origins.ConvertAll(
                    new Converter<Origin, OriginDTO>(x=>x.ToOriginDTO())),
            Labels = product.Labels.ConvertAll(
                    new Converter<Label, LabelDTO>(x=>x.ToLabelDTO())),
            Allergens = product.Allergens.ConvertAll(
                    new Converter<Allergen, AllergenDTO>(x=>x.ToAllergenDTO())),
            NutritionalInfo = product.NutritionInfo,
            NutritionGrade = product.NutritionGrade
        };
    }
}
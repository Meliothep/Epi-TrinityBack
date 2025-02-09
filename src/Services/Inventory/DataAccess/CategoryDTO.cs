using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

public class CategoryDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable instead of Optional<T>
    public string? Description { get; set; }


    public static CategoryDTO MakeDTO(Category category)
    {
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    public static Category MakeModel(CategoryDTO dto)
    {
        return new Category()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description
        };
    }
}
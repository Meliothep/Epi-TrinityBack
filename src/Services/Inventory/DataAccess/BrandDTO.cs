using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

public class BrandDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable to avoid serialization issues

    public static BrandDTO MakeDTO(Brand brand)
    {
        return new BrandDTO
        {
            Id = brand.Id,
            Name = brand.Name,
        };
    }

    public static Brand MakeModel(BrandDTO dto)
    {
        return new Brand()
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }
}

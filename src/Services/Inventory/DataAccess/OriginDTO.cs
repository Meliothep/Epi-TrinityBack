using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

public class OriginDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable instead of Optional<T>

    public static OriginDTO MakeDTO(Origin origin)
    {
        return new OriginDTO
        {
            Id = origin.Id,
            Name = origin.Name,
        };
    }

    public static Origin MakeModel(OriginDTO dto)
    {
        return new Origin()
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }
}

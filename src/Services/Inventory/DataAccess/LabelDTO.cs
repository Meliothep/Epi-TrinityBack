using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

public class LabelDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable to avoid serialization issues

    public static LabelDTO MakeDTO(Label allergen)
    {
        return new LabelDTO
        {
            Id = allergen.Id,
            Name = allergen.Name,
        };
    }

    public static Label MakeModel(LabelDTO dto)
    {
        return new Label()
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }
}

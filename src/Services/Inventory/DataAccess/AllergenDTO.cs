using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

public class AllergenDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable to avoid serialization issues


    public static AllergenDTO MakeDTO(Allergen allergen)
    {
        return new AllergenDTO
        {
            Id = allergen.Id,
            Name = allergen.Name,
        };
    }

    public static Allergen MakeModel(AllergenDTO dto)
    {
        return new Allergen()
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }
}

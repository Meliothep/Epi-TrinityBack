using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

namespace Inventory.DataAccess.DTOs;

public class AllergenDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable to avoid serialization issues
}

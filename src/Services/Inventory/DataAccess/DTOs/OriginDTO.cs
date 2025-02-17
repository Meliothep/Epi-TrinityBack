using System;
using System.Collections.Generic;
using System.Linq;
using Trinity.EntityModels.Models;

namespace Inventory.DataAccess.DTOs;

public class OriginDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; } // Nullable instead of Optional<T>
}

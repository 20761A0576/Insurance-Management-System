using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }

    public string? ProviderId { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}

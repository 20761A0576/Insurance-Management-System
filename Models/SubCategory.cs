using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class SubCategory
{
    public string SubCategoryId { get; set; } = null!;

    public string? SubCategoryName { get; set; }

    public string? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
}

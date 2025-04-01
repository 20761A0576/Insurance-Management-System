using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class Provider
{
    public string ProviderId { get; set; } = null!;

    public string? ProviderName { get; set; }

    public string? EmailId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CityId { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual City? City { get; set; }
}

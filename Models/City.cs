using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class City
{
    public string CityId { get; set; } = null!;

    public string? CityName { get; set; }

    public int? PinCode { get; set; }

    public string? StateId { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Provider> Providers { get; set; } = new List<Provider>();

    public virtual State? State { get; set; }
}

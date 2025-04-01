using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class State
{
    public string StateId { get; set; } = null!;

    public string? StateName { get; set; }

    public string? CapitalName { get; set; }

    public string? StateCode { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}

using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string? CustomerName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? EmailId { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PolicyId { get; set; }

    public string? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Policy? Policy { get; set; }
}

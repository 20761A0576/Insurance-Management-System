using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class Policy
{
    public string PolicyId { get; set; } = null!;

    public string PolicyName { get; set; }

    public decimal? SumAssured { get; set; }

    public decimal? PremiumAmount { get; set; }

    public int Tenure { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? SubCategoryId { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual SubCategory? SubCategory { get; set; }
}

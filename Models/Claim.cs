using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class Claim
{
    public string ClaimId { get; set; } = null!;

    public string? PolicyId { get; set; }

    public string? CustomerId { get; set; }

    public decimal? ClaimAmount { get; set; }

    public DateOnly? ClaimDate { get; set; }

    public string? ClaimStatus { get; set; }

    public string? ClaimReason { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Policy? Policy { get; set; }
}

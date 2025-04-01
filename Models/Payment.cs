using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string? CustomerId { get; set; }

    public string? PolicyId { get; set; }

    public string? PaymentType { get; set; }

    public DateOnly? DateOfPayment { get; set; }

    public string? Amount { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Policy? Policy { get; set; }
}

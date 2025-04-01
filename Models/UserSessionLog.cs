using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class UserSessionLog
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime LoginTime { get; set; }

    public DateTime? LogoutTime { get; set; }

    public int? Duration { get; set; }
}

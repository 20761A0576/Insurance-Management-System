using System;
using System.Collections.Generic;

namespace InsuranceProviderManagementSystem.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }
}

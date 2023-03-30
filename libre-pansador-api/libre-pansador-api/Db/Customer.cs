using System;
using System.Collections.Generic;

namespace libre_pansador_api.Db;

public partial class Customer
{
    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }
}

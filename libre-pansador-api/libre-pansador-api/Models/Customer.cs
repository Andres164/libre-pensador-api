using System;
using System.Collections.Generic;

namespace libre_pansador_api.Models;

public partial class Customer
{
    public string Email { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}

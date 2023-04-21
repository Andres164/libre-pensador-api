﻿using System;
using System.Collections.Generic;

namespace libre_pansador_api.Models;

public partial class Customer
{
    public string LoyverseCustomerId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public int total_points { get; set; } = 0;
    public DateTime DateOfBirth { get; set; }
}

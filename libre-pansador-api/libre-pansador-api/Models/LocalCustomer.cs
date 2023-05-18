using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace libre_pansador_api.Models;

public partial class LocalCustomer
{
    public string LoyverseCustomerId { get; set; } = null!;
    public string Email { get; set; } = null!;
    /*
    public DateOnly DateOfBirth 
    {
        get
        {
            return DateOnly.Parse(this.EncryptedDateOfBirth);
        }
        set
        {
            this.EncryptedDateOfBirth = value.ToString();
        }
    }*/
    public string EncryptedDateOfBirth { get; set; } = default!;
}

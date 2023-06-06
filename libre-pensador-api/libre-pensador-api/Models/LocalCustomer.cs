using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace libre_pensador_api.Models;

public partial class LocalCustomer
{
    public string LoyverseCustomerId { get; set; } = null!;
    public string EncryptedEmail { get; set; } = null!;
    public string DateOfBirth { get; set; } = default!;
    public string DecryptedEmail 
    {
        get => EncryptionUtility.Decrypt(this.EncryptedEmail)!;
        set
        {
            if(String.IsNullOrEmpty(value)) 
                throw new ArgumentNullException($"{nameof(value)} is null or empty");
            this.EncryptedEmail = EncryptionUtility.Encrypt(value)!;
        }
    }
}

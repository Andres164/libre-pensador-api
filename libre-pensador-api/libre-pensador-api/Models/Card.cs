using System;
using System.Collections.Generic;

namespace libre_pensador_api.Models;

public partial class Card
{
    public string CardId { get; set; } = null!;

    public string? EncryptedCustomerEmail { get; set; }
}

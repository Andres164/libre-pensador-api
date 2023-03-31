using System;
using System.Collections.Generic;

namespace libre_pansador_api.Models;

public partial class Card
{
    public string CardId { get; set; } = null!;

    public string? CustomerEmail { get; set; }
}

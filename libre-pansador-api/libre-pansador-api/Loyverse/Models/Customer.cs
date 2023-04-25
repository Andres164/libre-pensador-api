namespace libre_pansador_api.Loyverse.Models
{
    public class Customer
    {
        public string LoyverseCustomerId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int TotalPoints { get; set; } = 0;
    }
}

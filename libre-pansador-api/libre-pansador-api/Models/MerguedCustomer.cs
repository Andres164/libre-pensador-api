using System.Net;
using System.Xml.Linq;

namespace libre_pansador_api.Models
{
    public class MergedCustomer : Loyverse.Models.LoyverseCustomer
    {
        public string DateOfBirth { get; set; }

        public MergedCustomer(Models.LocalCustomer localCustomer, Loyverse.Models.LoyverseCustomer loyverseCustomer)
        {
            this.LoyverseCustomerId = loyverseCustomer.LoyverseCustomerId;
            this.Name = loyverseCustomer.Name;
            this.Phone = loyverseCustomer.Phone;
            this.Address = loyverseCustomer.Address;
            this.TotalPoints = loyverseCustomer.TotalPoints;
            this.Email = loyverseCustomer.Email;

            this.DateOfBirth = localCustomer.DateOfBirth;
        }
    }

}

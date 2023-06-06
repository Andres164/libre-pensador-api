using libre_pensador_api.Models;
using libre_pensador_api.Models.ViewModels;

namespace libre_pensador_api.Mappers
{
    public static class LocalCustomerMapper
    {
        public static LocalCustomerViewModel ToViewModel(LocalCustomer localCustomer)
        {
            return new LocalCustomerViewModel
            {
                LoyverseCustomerId = localCustomer.LoyverseCustomerId,
                Email = localCustomer.DecryptedEmail,
                DateOfBirth = localCustomer.DateOfBirth
            };
        }

        public static LocalCustomer ToModel(LocalCustomerViewModel localCustomerView)
        {
            return new LocalCustomer
            {
                LoyverseCustomerId = localCustomerView.LoyverseCustomerId,
                DecryptedEmail = localCustomerView.Email,
                DateOfBirth = localCustomerView.DateOfBirth
            };
        }
    }
}

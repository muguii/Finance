using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class AddressToAddressViewModel
    {
        public static AddressDetailsViewModel ToAddressDetailsViewModel(this Address address)
        {
            return new AddressDetailsViewModel(address.Street, address.Number, address.PostalCode, address.District, address.City, address.State, address.Country);
        }
    }
}

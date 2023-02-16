using Finance.Application.ViewModels.Address;

namespace Finance.Application.Mappers.Address
{
    public static class AddressToAddressViewModel
    {
        public static AddressDetailsViewModel ToAddressDetailsViewModel(this Core.Entities.Address address)
        {
            return new AddressDetailsViewModel(address.Street, address.Number, address.PostalCode, address.District, address.City, address.State, address.Country);
        }
    }
}

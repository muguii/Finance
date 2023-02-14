namespace Finance.Application.ViewModels
{
    public class AddressDetailsViewModel
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string PostalCode { get; private set; }
        public string District { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        public AddressDetailsViewModel(string street, string number, string postalCode, string district, string city, string state, string country)
        {
            Street = street;
            Number = number;
            PostalCode = postalCode;
            District = district;
            City = city;
            State = state;
            Country = country;
        }
    }
}

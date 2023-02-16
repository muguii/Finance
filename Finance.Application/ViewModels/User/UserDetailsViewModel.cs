using Finance.Application.ViewModels.Account;
using Finance.Application.ViewModels.Address;
using Finance.Application.ViewModels.Category;

namespace Finance.Application.ViewModels.User
{
    public class UserDetailsViewModel
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string? Gender { get; private set; }
        public string Telephone { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public AddressDetailsViewModel Address { get; private set; }
        public List<AccountDetailsViewModel> Accounts { get; private set; }
        public List<CategoryDetailsViewModel> Categories { get; private set; }

        public UserDetailsViewModel(int id,
                                    string login,
                                    string email,
                                    string fullName,
                                    DateTime birthdate,
                                    string gender,
                                    string telephone,
                                    bool active,
                                    DateTime createdAt,
                                    DateTime lastUpdate,
                                    AddressDetailsViewModel address,
                                    List<AccountDetailsViewModel> accounts,
                                    List<CategoryDetailsViewModel> categories)
        {
            Id = id;
            Login = login;
            Email = email;
            FullName = fullName;
            Birthdate = birthdate;
            Gender = gender;
            Telephone = telephone;
            Active = active;
            CreatedAt = createdAt;
            LastUpdate = lastUpdate;

            Address = address;
            Accounts = accounts;
            Categories = categories;
        }
    }
}

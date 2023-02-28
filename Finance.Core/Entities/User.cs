namespace Finance.Core.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string? LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string? Gender { get; private set; }
        public string Telephone { get; private set; }

        public bool Active { get; private set; }
        public string Role { get; private set; }

        public Address Address { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Category> Categories { get; private set; }

        public User(string login, string password, string email, string name, string lastName,
                    DateTime birthdate, string gender, string telephone, string role) : base()
        {
            Login = login;
            Password = password;
            Email = email;
            Name = name;
            LastName = lastName;
            Birthdate = birthdate;
            Gender = gender;
            Telephone = telephone;

            Active = true;
            Role = role;

            Accounts = new List<Account>();
            Categories = new List<Category>();
        }

        public User(string login, string password, string email, string name, string lastName,
                    DateTime birthdate, string gender, string telephone, string role,
                    Address address) : this(login, password, email, name, lastName, birthdate, gender, telephone, role)
        {
            Address = address;
        }

        public void Update(string name, string? lastName, string telephone, string street, string number,
                           string postalCode, string district, string city, string state, string country)
        {
            Name = name;
            LastName = lastName;
            Telephone = telephone;

            Address.Update(street, number, postalCode, district, city, state, country);

            LastUpdate = DateTime.Now;
        }
    }
}

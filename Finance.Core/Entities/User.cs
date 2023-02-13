namespace Finance.Core.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string? LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string? Gender { get; private set; }
        public string Telephone { get; private set; }
        public bool Active { get; private set; }

        public Address Address { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Category> Categories { get; private set; }

        public User(string login, string email, string name, string lastName, DateTime birthdate, string gender, string telephone, bool active) : base()
        {
            Login = login;
            Email = email;
            Name = name;
            LastName = lastName;
            Birthdate = birthdate;
            Gender = gender;
            Telephone = telephone;
            Active = active;
        }
    }
}

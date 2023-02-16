namespace Finance.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string? Gender { get; private set; }

        public UserViewModel(int id, string fullName, DateTime birthdate, string gender)
        {
            Id = id;
            FullName = fullName;
            Birthdate = birthdate;
            Gender = gender;
        }
    }
}

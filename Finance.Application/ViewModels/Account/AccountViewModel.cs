namespace Finance.Application.ViewModels.Account
{
    public class AccountViewModel
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Color { get; private set; }
        public decimal Balance { get; private set; }

        public AccountViewModel(int id, string description, string color, decimal balance)
        {
            Id = id;
            Description = description;
            Color = color;
            Balance = balance;
        }
    }
}

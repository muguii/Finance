namespace Finance.Application.ViewModels
{
    public class AccountViewModel
    {
        public string Description { get; private set; }
        public string Color { get; private set; }
        public decimal Balance { get; private set; }

        public AccountViewModel(string description, string color, decimal balance)
        {
            Description = description;
            Color = color;
            Balance = balance;
        }
    }
}

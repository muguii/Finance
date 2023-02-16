namespace Finance.Application.ViewModels
{
    public class AccountDetailsViewModel
    {
        public string Description { get; private set; }
        public string Color { get; private set; }
        public decimal Balance { get; private set; }
        public decimal InitialBalance { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public List<TransactionDetailsViewModel> Transactions { get; private set; }

        public AccountDetailsViewModel(string description,
                                       string color,
                                       decimal balance,
                                       decimal initialBalance,
                                       bool active,
                                       DateTime createdAt,
                                       DateTime lastUpdate,
                                       List<TransactionDetailsViewModel> transactionDetailsViewModel)
        {
            Description = description;
            Color = color;
            Balance = balance;
            InitialBalance = initialBalance;
            Active = active;
            CreatedAt = createdAt;
            LastUpdate = lastUpdate;
            Transactions = transactionDetailsViewModel;
        }
    }
}

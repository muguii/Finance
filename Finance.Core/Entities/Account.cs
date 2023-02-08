namespace Finance.Core.Entities
{
    public class Account : BaseEntity
    {
        public string Description { get; private set; }
        public string Color { get; private set; }
        public decimal Balance { get; private set; }
        public decimal InitialBalance { get; private set; }
        public bool Active { get; private set; }

        public int UserId { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string description, string color, decimal balance, decimal initialBalance, bool active) : base()
        {
            Description = description;
            Color = color;
            Balance = balance;
            InitialBalance = initialBalance;
            Active = active;
        }
    }
}

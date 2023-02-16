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

        public Account(string description, string color, decimal balance, decimal initialBalance, int userId) : base()
        {
            Description = description;
            Color = color;
            Balance = balance;
            InitialBalance = initialBalance;
            UserId = userId;

            Active = true;

            Transactions= new List<Transaction>();
        }

        public void Update(string description, string color, decimal balance)
        {
            Description = description;
            Color = color; 
            Balance = balance;

            LastUpdate= DateTime.Now;
        }

        public void Shelve()
        {
            Active = false;
        }

        public void Unshelve()
        {
            Active = true;
        }
    }
}

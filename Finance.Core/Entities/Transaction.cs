namespace Finance.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Value { get; private set; }

        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }

        public Transaction(string description, DateTime date, decimal value, int accountId, int categoryId) : base()
        {
            Description = description;
            Date = date;
            Value = value;
            AccountId = accountId;
            CategoryId = categoryId;
        }

        public Transaction(string description, DateTime date, decimal value, int accountId, int categoryId, Account account, Category category) : this(description, date, value, accountId, categoryId)
        {
            Account = account;
            Category = category;
        }

        public void Update(string description, DateTime date, decimal value)
        {
            Description = description;
            Date = date;
            Value = value;

            LastUpdate = DateTime.Now;
        }
    }
}

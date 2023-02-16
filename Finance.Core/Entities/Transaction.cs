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

        public void Update(int categoryId, int accountId, string description, DateTime date)
        {
            AccountId = accountId;
            CategoryId = categoryId;
            Description = description;
            Date = date;

            LastUpdate = DateTime.Now;
        }
    }
}

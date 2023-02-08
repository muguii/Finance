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

        public Transaction(string description, DateTime date, decimal value) : base()
        {
            Description = description;
            Date = date;
            Value = value;
        }
    }
}

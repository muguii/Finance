namespace Finance.Application.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public TransactionViewModel(string description, DateTime date, decimal value, DateTime createdAt, DateTime lastUpdate)
        {
            Description = description;
            Date = date;
            Value = value;
            CreatedAt = createdAt;
            LastUpdate = lastUpdate;
        }
    }
}

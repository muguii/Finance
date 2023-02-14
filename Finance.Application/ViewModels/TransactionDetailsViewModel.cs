namespace Finance.Application.ViewModels
{
    public class TransactionDetailsViewModel
    {
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public CategoryViewModel Category { get; private set; }

        public TransactionDetailsViewModel(string description, DateTime date, decimal value, DateTime createdAt, DateTime lastUpdate)
        {
            Description = description;
            Date = date;
            Value = value;
            CreatedAt = createdAt; 
            LastUpdate = lastUpdate;
        }
    }
}

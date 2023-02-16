using Finance.Application.ViewModels.Account;
using Finance.Application.ViewModels.Category;

namespace Finance.Application.ViewModels.Transaction
{
    public class TransactionDetailsViewModel
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public AccountViewModel Account { get; private set; }
        public CategoryViewModel Category { get; private set; }

        public TransactionDetailsViewModel(int id,
                                           string description,
                                           DateTime date,
                                           decimal value,
                                           DateTime createdAt,
                                           DateTime lastUpdate,
                                           AccountViewModel accountViewModel,
                                           CategoryViewModel categoryViewModel)
        {
            Id = id;
            Description = description;
            Date = date;
            Value = value;
            CreatedAt = createdAt;
            LastUpdate = lastUpdate;
            Account = accountViewModel;
            Category = categoryViewModel;
        }
    }
}

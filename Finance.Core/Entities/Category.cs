using Finance.Core.Enums;

namespace Finance.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; private set; }
        public string Color { get; private set; }
        public bool Active { get; private set; }
        public CategoryType Type { get; private set; }

        public int UserId { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Category(string description, string color, bool active, CategoryType type) : base()
        {
            Description = description;
            Color = color;
            Active = active;
            Type = type;
        }
    }
}

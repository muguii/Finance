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

        public Category(string description, string color, CategoryType type, int userId) : base()
        {
            Description = description;
            Color = color;
            Type = type;
            UserId = userId;

            Active = true;

            Transactions = new List<Transaction>();
        }

        public void Update(string description, string color)
        {
            Description = description;
            Color = color;

            LastUpdate = DateTime.Now;
        }

        public void Shelve()
        {
            Active = false;

            LastUpdate = DateTime.Now;
        }

        public void Unshelve()
        {
            Active = true;

            LastUpdate = DateTime.Now;
        }
    }
}

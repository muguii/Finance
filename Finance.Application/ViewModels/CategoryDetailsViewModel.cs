using Finance.Core.Enums;

namespace Finance.Application.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public string Description { get; private set; }
        public string Color { get; private set; }
        public bool Active { get; private set; }
        public CategoryType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public CategoryDetailsViewModel(string description, string color, bool active, CategoryType type, DateTime createdAt, DateTime lastUpdate)
        {
            Description = description;
            Color = color;
            Active = active;
            Type = type;
            CreatedAt = createdAt;
            LastUpdate = lastUpdate;
        }
    }
}

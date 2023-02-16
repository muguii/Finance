using Finance.Core.Enums;

namespace Finance.Application.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Color { get; private set; }
        public CategoryType Type { get; private set; }

        public CategoryViewModel(int id, string description, string color, CategoryType type)
        {
            Id = id;
            Description = description;
            Color = color;
            Type = type;
        }
    }
}

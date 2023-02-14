using Finance.Core.Enums;

namespace Finance.Application.ViewModels
{
    public class CategoryViewModel
    {
        public string Description { get; private set; }
        public CategoryType Type { get; private set; }

        public CategoryViewModel(string description, CategoryType type)
        {
            Description = description;
            Type = type;
        }
    }
}

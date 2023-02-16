using Finance.Application.ViewModels;

namespace Finance.Application.Mappers.Category
{
    public static class CategoryToCategoryViewModel
    {
        public static CategoryViewModel ToCategoryViewModel(this Core.Entities.Category category)
        {
            return new CategoryViewModel(category.Description, category.Color, category.Type);
        }
    }
}

using Finance.Application.ViewModels.Category;

namespace Finance.Application.Mappers.Category
{
    public static class CategoryToCategoryViewModel
    {
        public static CategoryViewModel ToCategoryViewModel(this Core.Entities.Category category)
        {
            return new CategoryViewModel(category.Id, category.Description, category.Color, category.Type);
        }
    }
}

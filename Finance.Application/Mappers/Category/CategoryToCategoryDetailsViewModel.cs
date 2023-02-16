using Finance.Application.ViewModels;

namespace Finance.Application.Mappers.Category
{
    public static class CategoryToCategoryDetailsViewModel
    {
        public static CategoryDetailsViewModel ToCategoryDetailsViewModel(this Core.Entities.Category category)
        {
            return new CategoryDetailsViewModel(category.Description, category.Color, category.Active, category.Type, category.CreatedAt, category.LastUpdate);
        }
    }
}

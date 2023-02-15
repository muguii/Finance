using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class CategoryToCategoryDetailsViewModel
    {
        public static CategoryDetailsViewModel ToCategoryDetailsViewModel(this Category category)
        {
            return new CategoryDetailsViewModel(category.Description, category.Color, category.Active, category.Type, category.CreatedAt, category.LastUpdate);
        }
    }
}

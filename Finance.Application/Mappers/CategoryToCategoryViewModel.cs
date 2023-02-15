using Finance.Application.ViewModels;
using Finance.Core.Entities;

namespace Finance.Application.Mappers
{
    public static class CategoryToCategoryViewModel
    {
        public static CategoryViewModel ToCategoryViewModel(this Category category)
        {
            return new CategoryViewModel(category.Description, category.Color, category.Type);
        }
    }
}

using OfficeSharedShopping.Services.DTOs.ProductCategory;

namespace OfficeSharedShopping.Services.Interfaces.ProductCategory
{
    public interface IProductCategoryService
    {
        Task<GetProductCategoryResponse> GetByIdAsync(int categoryId);

        Task<GetAllProductCategoriesResponse> GetAllAsync();
    }
}

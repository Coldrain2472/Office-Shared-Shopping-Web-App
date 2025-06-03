using OfficeSharedShopping.Repository.Interfaces.ProductCategory;
using OfficeSharedShopping.Services.DTOs.ProductCategory;
using OfficeSharedShopping.Services.Interfaces.ProductCategory;

namespace OfficeSharedShopping.Services.Implementations.ProductCategory
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository _productCategoryRepository)
        {
            productCategoryRepository = _productCategoryRepository;
        }

        public async Task<GetAllProductCategoriesResponse> GetAllAsync()
        {
            var productCategories = await productCategoryRepository.RetrieveCollectionAsync(new ProductCategoryFilter()).ToListAsync();
            var allProductCategoriesInfo = new GetAllProductCategoriesResponse()
            {
                ProductCategories = productCategories.Select(MapToProductCategoryInfo).ToList(),
                TotalCount = productCategories.Count
            };

            return allProductCategoriesInfo;
        }

        public async Task<GetProductCategoryResponse> GetByIdAsync(int categoryId)
        {
            var productCategory = await productCategoryRepository.RetrieveAsync(categoryId);
            var productCategoryInfo = MapToProductCategoryInfo(productCategory) as GetProductCategoryResponse;
            return productCategoryInfo;
        }

        private ProductCategoryInfo MapToProductCategoryInfo(Models.ProductCategory category)
        {
            return new ProductCategoryInfo
            {
                Name = category.Name,
                ProductCategoryId = category.ProductCategoryId,
            };
        }
    }
}

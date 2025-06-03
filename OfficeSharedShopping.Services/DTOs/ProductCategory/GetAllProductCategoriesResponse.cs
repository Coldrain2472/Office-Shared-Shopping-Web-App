namespace OfficeSharedShopping.Services.DTOs.ProductCategory
{
    public class GetAllProductCategoriesResponse
    {
        public List<ProductCategoryInfo> ProductCategories { get; set; }

        public int TotalCount { get; set; }
    }
}

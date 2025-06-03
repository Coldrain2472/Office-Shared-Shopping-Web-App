namespace OfficeSharedShopping.Services.DTOs.Product
{
    public class GetAllProductsResponse
    {
        public List<ProductInfo>? Products { get; set; }

        public int TotalCount { get; set; }
    }
}

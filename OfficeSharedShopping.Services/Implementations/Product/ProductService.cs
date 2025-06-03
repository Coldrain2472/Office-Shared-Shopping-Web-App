using OfficeSharedShopping.Repository.Interfaces;
using OfficeSharedShopping.Repository.Interfaces.Product;
using OfficeSharedShopping.Services.DTOs.Product;
using OfficeSharedShopping.Services.Interfaces.Product;

namespace OfficeSharedShopping.Services.Implementations.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public async Task<GetAllProductsResponse> GetAllAsync()
        {
            var products = await productRepository.RetrieveCollectionAsync(new ProductFilter()).ToListAsync();

           var allProductsResponse = new GetAllProductsResponse()
           {
               Products = products.Select(MapToProductInfo).ToList(),
               TotalCount = products.Count
           };

            return allProductsResponse;
        }

        public async Task<GetProductResponse> GetByIdAsync(int productId)
        {
            var product = await productRepository.RetrieveAsync(productId);

            var productInfo = MapToProductInfo(product) as GetProductResponse;
            return productInfo;
        }

        private ProductInfo MapToProductInfo(Models.Product product)
        {
            return new ProductInfo
            {
                CategoryId = product.CategoryId,
                Name = product.Name,
                ProductId = product.ProductId
            };
        }
    }
}

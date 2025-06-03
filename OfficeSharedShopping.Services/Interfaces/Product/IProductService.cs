using OfficeSharedShopping.Services.DTOs.Product;

namespace OfficeSharedShopping.Services.Interfaces.Product
{
    public interface IProductService
    {
        Task<GetProductResponse> GetByIdAsync(int productId);

        Task<GetAllProductsResponse> GetAllAsync();
    }
}

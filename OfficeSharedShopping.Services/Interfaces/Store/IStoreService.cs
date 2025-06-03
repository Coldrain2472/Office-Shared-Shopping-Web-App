using OfficeSharedShopping.Services.DTOs.Store;

namespace OfficeSharedShopping.Services.Interfaces.Store
{
    public interface IStoreService
    {
        Task<GetStoreResponse> GetByIdAsync(int storeId);

        Task<GetAllStoresResponse> GetAllAsync();
    }
}

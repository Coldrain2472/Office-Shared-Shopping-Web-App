using OfficeSharedShopping.Repository.Interfaces.Store;
using OfficeSharedShopping.Services.DTOs.Store;
using OfficeSharedShopping.Services.Interfaces.Store;

namespace OfficeSharedShopping.Services.Implementations.Store
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository storeRepository;

        public StoreService(IStoreRepository _storeRepository)
        {
            storeRepository = _storeRepository;
        }

        public async Task<GetAllStoresResponse> GetAllAsync()
        {
            var stores = await storeRepository.RetrieveCollectionAsync(new StoreFilter()).ToListAsync();
            var allStoresInfo = new GetAllStoresResponse
            {
                Stores = stores.Select(MapToStoreInfo).ToList(),
                TotalCount = stores.Count
            };

            return allStoresInfo;
        }

        public async Task<GetStoreResponse> GetByIdAsync(int storeId)
        {
            var store = await storeRepository.RetrieveAsync(storeId);
            return new GetStoreResponse
            {
                StoreId = storeId,
                Name = store.Name
            };
        }

        private StoreInfo MapToStoreInfo(Models.Store store)
        {
            return new StoreInfo
            {
                StoreId = store.StoreId,
                Name = store.Name
            };
        }
    }
}

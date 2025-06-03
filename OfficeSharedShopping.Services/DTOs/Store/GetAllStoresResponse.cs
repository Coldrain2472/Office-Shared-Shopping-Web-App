namespace OfficeSharedShopping.Services.DTOs.Store
{
    public class GetAllStoresResponse
    {
        public List<StoreInfo>? Stores { get; set; }

        public int TotalCount { get; set; }
    }
}

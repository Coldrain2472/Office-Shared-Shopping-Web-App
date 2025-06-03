namespace OfficeSharedShopping.Services.DTOs.ShoppingSession
{
    public class GetEndedShoppingSessionsHistoryResponse
    {
        public List<ShoppingSessionInfo>? Sessions { get; set; }

        public int TotalCount { get; set; }
    }
}

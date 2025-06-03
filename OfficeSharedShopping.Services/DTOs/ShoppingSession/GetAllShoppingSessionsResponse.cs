namespace OfficeSharedShopping.Services.DTOs.ShoppingSession
{
    public class GetAllShoppingSessionsResponse
    {
        public List<ShoppingSessionInfo>? ShoppingSessions { get; set; }

        public int TotalCount { get; set; }
    }
}

namespace OfficeSharedShopping.Services.DTOs.ShoppingSession
{
    public class EndShoppingSessionRequest
    {
        public int ShoppingSessionId { get; set; }

        public int CreatedByEmployeeId { get; set; }
    }
}

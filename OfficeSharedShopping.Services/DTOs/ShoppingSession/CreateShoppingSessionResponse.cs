namespace OfficeSharedShopping.Services.DTOs.ShoppingSession
{
    public class CreateShoppingSessionResponse : ShoppingSessionInfo
    {
        public string? ErrorMessage { get; set; }

        public bool Success { get; set; }
    }
}

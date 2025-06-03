namespace OfficeSharedShopping.Services.DTOs.ShoppingSession
{
    public class EndShoppingSessionResponse
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime? EndTime { get; set; }
    }
}

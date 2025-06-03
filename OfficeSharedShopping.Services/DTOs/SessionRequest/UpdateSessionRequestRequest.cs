namespace OfficeSharedShopping.Services.DTOs.SessionRequest
{
    public class UpdateSessionRequestRequest
    {
        public int Quantity { get; set; }

        public decimal MaxPrice { get; set; }

        public int ShoppingSessionId { get; set; }

        public int ProductId { get; set; }
    }
}

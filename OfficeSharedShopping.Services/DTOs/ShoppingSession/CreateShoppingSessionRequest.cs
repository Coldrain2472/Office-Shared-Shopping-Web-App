namespace OfficeSharedShopping.Services.DTOs.ShoppingSession
{
    public class CreateShoppingSessionRequest
    {
        public int ShoppingSessionId { get; set; }

        public int StoreId { get; set; }

        public string? StoreName { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public string? CreatedByEmployeeName { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

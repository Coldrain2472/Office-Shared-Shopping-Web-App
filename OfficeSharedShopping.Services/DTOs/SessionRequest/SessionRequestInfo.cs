namespace OfficeSharedShopping.Services.DTOs.SessionRequest
{
    public class SessionRequestInfo
    {
        public int SessionRequestId { get; set; }

        public int SessionId { get; set; }

        public int EmployeeId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal MaxPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public string EmployeeName { get; set; }

        public string ProductName { get; set; }

        public bool IsActive { get; set; }
    }
}
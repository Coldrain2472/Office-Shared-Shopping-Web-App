namespace OfficeSharedShopping.Services.DTOs.SessionRequest
{
    public class SessionRequestDetails
    {
        public string EmployeeName { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal MaxPrice { get; set; }
    }
}

namespace OfficeSharedShopping.Services.DTOs.SessionRequest
{
    public class UpdateSessionRequestResponse : SessionRequestInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}

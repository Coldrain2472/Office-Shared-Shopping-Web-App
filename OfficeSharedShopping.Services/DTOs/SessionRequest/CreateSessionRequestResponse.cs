namespace OfficeSharedShopping.Services.DTOs.SessionRequest
{
    public class CreateSessionRequestResponse : SessionRequestInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}

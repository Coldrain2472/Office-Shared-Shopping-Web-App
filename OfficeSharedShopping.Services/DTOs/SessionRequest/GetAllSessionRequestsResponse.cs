namespace OfficeSharedShopping.Services.DTOs.SessionRequest
{
    public class GetAllSessionRequestsResponse
    {
        public List<SessionRequestInfo>? SessionRequests { get; set; }

        public int TotalCount { get; set; }
    }
}

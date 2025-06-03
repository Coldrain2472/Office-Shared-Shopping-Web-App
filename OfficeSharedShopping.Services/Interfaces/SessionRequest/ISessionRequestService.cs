using OfficeSharedShopping.Services.DTOs.SessionRequest;

namespace OfficeSharedShopping.Services.Interfaces.SessionRequest
{
    public interface ISessionRequestService
    {
        Task<GetSessionRequestResponse> GetByIdAsync(int requestId);

        Task<GetAllSessionRequestsResponse> GetAllAsync();

        Task<CreateSessionRequestResponse> CreateSessionRequest(CreateSessionRequestRequest request);

        Task<UpdateSessionRequestResponse> UpdateSessionRequest(int sessionRequestId, UpdateSessionRequestRequest request);

        Task<List<SessionRequestDetails>> GetDetailsForShoppingSessionAsync(int shoppingSessionId);

    }
}

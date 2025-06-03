using OfficeSharedShopping.Services.DTOs.ShoppingSession;

namespace OfficeSharedShopping.Services.Interfaces.ShoppingSession
{
    public interface IShoppingSessionService
    {
        Task<GetShoppingSessionResponse> GetByIdAsync(int sessionId);

        Task<GetAllShoppingSessionsResponse> GetAllAsync();

        Task<CreateShoppingSessionResponse> CreateShoppingSessionAsync(CreateShoppingSessionRequest request);

        Task<EndShoppingSessionResponse> EndShoppingSessionAsync(EndShoppingSessionRequest request);

        Task<GetEndedShoppingSessionsHistoryResponse> GetEndedShoppingSessionsHistory();
    }
}

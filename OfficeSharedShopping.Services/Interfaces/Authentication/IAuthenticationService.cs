using OfficeSharedShopping.Services.DTOs.Authentication;

namespace OfficeSharedShopping.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}

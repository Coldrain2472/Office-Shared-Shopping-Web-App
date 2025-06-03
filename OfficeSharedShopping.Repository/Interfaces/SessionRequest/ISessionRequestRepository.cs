using OfficeSharedShopping.Repository.Base;

namespace OfficeSharedShopping.Repository.Interfaces.SessionRequest
{
    public interface ISessionRequestRepository : IBaseRepository<Models.SessionRequest, SessionRequestFilter, SessionRequestUpdate>
    {
    }
}

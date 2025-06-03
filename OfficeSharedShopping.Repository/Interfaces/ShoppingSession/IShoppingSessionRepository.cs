using OfficeSharedShopping.Repository.Base;

namespace OfficeSharedShopping.Repository.Interfaces.ShoppingSession
{
    public interface IShoppingSessionRepository : IBaseRepository<Models.ShoppingSession, ShoppingSessionFilter, ShoppingSessionUpdate>
    {
    }
}

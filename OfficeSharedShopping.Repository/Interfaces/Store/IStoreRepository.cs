using OfficeSharedShopping.Models;
using OfficeSharedShopping.Repository.Base;

namespace OfficeSharedShopping.Repository.Interfaces.Store
{
    public interface IStoreRepository : IBaseRepository<Models.Store, StoreFilter, StoreUpdate>
    {
    }
}

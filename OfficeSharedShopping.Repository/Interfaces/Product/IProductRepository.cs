using OfficeSharedShopping.Models;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Interfaces.Product;

namespace OfficeSharedShopping.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository<Models.Product, ProductFilter, ProductUpdate>
    {
    }
}

using OfficeSharedShopping.Models;
using OfficeSharedShopping.Repository.Base;

namespace OfficeSharedShopping.Repository.Interfaces.ProductCategory
{
    public interface IProductCategoryRepository : IBaseRepository<Models.ProductCategory, ProductCategoryFilter, ProductCategoryUpdate>
    {
    }
}

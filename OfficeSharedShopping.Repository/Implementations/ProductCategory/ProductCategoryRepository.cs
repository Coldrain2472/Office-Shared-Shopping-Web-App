using Microsoft.Data.SqlClient;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Helpers;
using OfficeSharedShopping.Repository.Interfaces.ProductCategory;

namespace OfficeSharedShopping.Repository.Implementations.ProductCategory
{
    public class ProductCategoryRepository : BaseRepository<Models.ProductCategory>, IProductCategoryRepository
    {
        private const string IdDbFieldEnumeratorName = "ProductCategoryId";

        protected override string GetTableName()
        {
            return "ProductCategories";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name"
        };

        protected override Models.ProductCategory MapEntity(SqlDataReader reader)
        {
            return new Models.ProductCategory
            {
                ProductCategoryId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"])
            };
        }

        public Task<int> CreateAsync(Models.ProductCategory entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.ProductCategory> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.ProductCategory> RetrieveCollectionAsync(ProductCategoryFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ProductCategoryUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "ProductCategories", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
using Microsoft.Data.SqlClient;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Helpers;
using OfficeSharedShopping.Repository.Interfaces;
using OfficeSharedShopping.Repository.Interfaces.Product;

namespace OfficeSharedShopping.Repository.Implementations.Product
{
    public class ProductRepository : BaseRepository<Models.Product>, IProductRepository
    {
        private const string IdDbFieldEnumeratorName = "ProductId";

        protected override string GetTableName()
        {
            return "Products";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "CategoryId"
        };

        protected override Models.Product MapEntity(SqlDataReader reader)
        {
            return new Models.Product
            {
                ProductId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"])
            };
        }

        public Task<int> CreateAsync(Models.Product entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Product> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Product> RetrieveCollectionAsync(ProductFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ProductUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Products", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
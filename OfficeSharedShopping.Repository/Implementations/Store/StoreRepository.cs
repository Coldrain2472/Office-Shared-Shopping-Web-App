using Microsoft.Data.SqlClient;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Helpers;
using OfficeSharedShopping.Repository.Interfaces.Store;

namespace OfficeSharedShopping.Repository.Implementations.Store
{
    public class StoreRepository : BaseRepository<Models.Store>, IStoreRepository
    {
        private const string IdDbFieldEnumeratorName = "StoreId";

        protected override string GetTableName()
        {
            return "Stores";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name"
        };

        protected override Models.Store MapEntity(SqlDataReader reader)
        {
            return new Models.Store
            {
                StoreId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"])
            };
        }

        public Task<int> CreateAsync(Models.Store entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Store> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Store> RetrieveCollectionAsync(StoreFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, StoreUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Stores", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}

using Microsoft.Data.SqlClient;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Helpers;
using OfficeSharedShopping.Repository.Interfaces.ShoppingSession;

namespace OfficeSharedShopping.Repository.Implementations.ShoppingSession
{
    public class ShoppingSessionRepository : BaseRepository<Models.ShoppingSession>, IShoppingSessionRepository
    {
        private const string IdDbFieldEnumeratorName = "ShoppingSessionId";

        protected override string GetTableName()
        {
            return "ShoppingSessions";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "StoreId",
            "CreatedByEmployeeId",
            "Deadline",
            "IsActive",
            "CreatedAt"
        };

        protected override Models.ShoppingSession MapEntity(SqlDataReader reader)
        {
            return new Models.ShoppingSession
            {
                ShoppingSessionId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                StoreId = Convert.ToInt32(reader["StoreId"]),
                CreatedByEmployeeId = Convert.ToInt32(reader["CreatedByEmployeeId"]),
                Deadline = Convert.ToDateTime(reader["Deadline"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }

        public Task<int> CreateAsync(Models.ShoppingSession entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.ShoppingSession> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.ShoppingSession> RetrieveCollectionAsync(ShoppingSessionFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.IsActive is not null)
            {
                commandFilter.AddCondition("IsActive", filter.IsActive);
            }
            if (filter.CreatedByEmployeeId is not null)
            {
                commandFilter.AddCondition("CreatedByEmployeeId", filter.CreatedByEmployeeId);
            }
            if (filter.StoreId is not null) 
            {
                commandFilter.AddCondition("StoreId", filter.StoreId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ShoppingSessionUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "ShoppingSessions", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("IsActive", update.IsActive);
            updateCommand.AddSetClause("Deadline", update.Deadline);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
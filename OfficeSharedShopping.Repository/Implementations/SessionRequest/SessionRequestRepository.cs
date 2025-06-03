using Microsoft.Data.SqlClient;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Helpers;
using OfficeSharedShopping.Repository.Interfaces.SessionRequest;

namespace OfficeSharedShopping.Repository.Implementations.SessionRequest
{
    public class SessionRequestRepository : BaseRepository<Models.SessionRequest>, ISessionRequestRepository
    {
        private const string IdDbFieldEnumeratorName = "SessionRequestId";

        protected override string GetTableName()
        {
            return "SessionRequests";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "SessionId",
            "EmployeeId",
            "ProductId",
            "Quantity",
            "MaxPrice",
            "CreatedAt"
        };

        protected override Models.SessionRequest MapEntity(SqlDataReader reader)
        {
            return new Models.SessionRequest
            {
                SessionRequestId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                SessionId = Convert.ToInt32(reader["SessionId"]),
                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                ProductId = Convert.ToInt32(reader["ProductId"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                MaxPrice = Convert.ToDecimal(reader["MaxPrice"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }

        public Task<int> CreateAsync(Models.SessionRequest entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.SessionRequest> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.SessionRequest> RetrieveCollectionAsync(SessionRequestFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.SessionId is not null)
            {
                commandFilter.AddCondition("SessionId", filter.SessionId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, SessionRequestUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "SessionRequests", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Quantity", update.Quantity);
            updateCommand.AddSetClause("MaxPrice", update.MaxPrice);
            updateCommand.AddSetClause("ProductId", update.ProductId);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
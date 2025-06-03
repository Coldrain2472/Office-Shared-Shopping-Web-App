using Microsoft.Data.SqlClient;
using OfficeSharedShopping.Repository.Base;
using OfficeSharedShopping.Repository.Helpers;
using OfficeSharedShopping.Repository.Interfaces.Employee;

namespace OfficeSharedShopping.Repository.Implementations.Employee
{
    public class EmployeeRepository : BaseRepository<Models.Employee>, IEmployeeRepository
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";

        protected override string GetTableName()
        {
            return "Employees";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "Email",
            "Password",
            "Department",
            "Phone"
        };

        protected override Models.Employee MapEntity(SqlDataReader reader)
        {
            return new Models.Employee
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Email = Convert.ToString(reader["Email"]),
                Password = Convert.ToString(reader["Password"]),
                Department = Convert.ToString(reader["Department"]),
                Phone = Convert.ToString(reader["Phone"])
            };
        }

        public Task<int> CreateAsync(Models.Employee entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Employee> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Employee> RetrieveCollectionAsync(EmployeeFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Email is not null)
            {
                commandFilter.AddCondition("Email", filter.Email);
            }
            if (filter.Department is not null)
            {
                commandFilter.AddCondition("Department", filter.Department);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Employees", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Email", update.Email);
            updateCommand.AddSetClause("Department", update.Department);
            updateCommand.AddSetClause("Phone", update.Phone);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}

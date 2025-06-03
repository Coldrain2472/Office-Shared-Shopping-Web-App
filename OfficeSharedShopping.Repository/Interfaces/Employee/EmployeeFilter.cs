using System.Data.SqlTypes;

namespace OfficeSharedShopping.Repository.Interfaces.Employee
{
    public class EmployeeFilter
    {
        public SqlString? Email { get; set; }

        public SqlString? Department { get; set; }
    }
}

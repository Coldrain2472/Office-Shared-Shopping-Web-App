using System.Data.SqlTypes;

namespace OfficeSharedShopping.Repository.Interfaces.Employee
{
    public class EmployeeUpdate
    {
        public SqlString? Name { get; set; }

        public SqlString? Email { get; set; }

        public SqlString? Department { get; set; }

        public SqlString? Phone { get; set; }

        public SqlString? Password { get; set; }
    }
}

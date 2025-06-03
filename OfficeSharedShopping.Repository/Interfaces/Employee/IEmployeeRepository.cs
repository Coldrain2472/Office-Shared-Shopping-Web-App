using OfficeSharedShopping.Repository.Base;

namespace OfficeSharedShopping.Repository.Interfaces.Employee
{
    public interface IEmployeeRepository : IBaseRepository<Models.Employee, EmployeeFilter, EmployeeUpdate>
    {
    }
}

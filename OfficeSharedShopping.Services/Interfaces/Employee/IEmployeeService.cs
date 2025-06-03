using OfficeSharedShopping.Services.DTOs.Employee;

namespace OfficeSharedShopping.Services.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<GetEmployeeResponse> GetByIdAsync(int employeeId);

        Task<GetAllEmployeesResponse> GetAllAsync();

        Task<UpdateEmployeeResponse> UpdateFullNameAsync(UpdateNameRequest request);

        Task<UpdateEmployeeResponse> UpdatePasswordAsync(UpdatePasswordRequest request);
    }
}
